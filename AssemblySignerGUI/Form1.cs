using Newtonsoft.Json;
using SKM.V3.Methods;
using SKM.V3.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace AssemblySignerGUI
{
    public partial class Form1 : Form
    {
        VendorConfig vendorConfig;

        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = openFileDialog1.FileName;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string asmDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            try
            {
                vendorConfig = JsonConvert.DeserializeObject<VendorConfig>(System.IO.File.ReadAllText(Path.Combine(asmDir, "config.json")));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: could not find config.json. Please contact the vendor.");
                return;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            SHA512 sha = SHA512.Create();

            var key = textBox1.Text;

            var response = Key.Activate(vendorConfig.ActivateToken, new ActivateModel { ProductId = vendorConfig.ProductId, Key = key , MachineCode = Helpers.GetMachineCodePI() });

            if (!Helpers.IsSuccessful(response))
            {
                MessageBox.Show($"Error: Could not activate the following device, {(response != null ? response.Message : "")}. Please contact the vendor to increase the number of end users for this license key.");
                return;
            }

            long dObjId = -1;
            foreach (var dObj in response.LicenseKey.DataObjects)
            {
                if (dObj.Name == "cryptolens_assemblyhash")
                {
                    dObjId = dObj.Id;
                    break;
                }
            }

            if (dObjId == -1)
            {
                var dObjRes = Data.AddDataObject(vendorConfig.DataObjectToken, new AddDataObjectToKeyModel
                {
                    Name = "cryptolens_assemblyhash",
                    ProductId = vendorConfig.ProductId,
                    StringValue = "",
                    Key = key,
                    CheckForDuplicates = true
                });

                if (!Helpers.IsSuccessful(dObjRes))
                {
                    Console.WriteLine($"Warning: Could not add data object, '{(dObjRes != null ? dObjRes.Message : "")}'.");
                }
                else
                {
                    dObjId = dObjRes.Id;
                }
            }

            var path = textBox2.Text;

            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                MessageBox.Show("Incorrect path. File not found.");
                return;
            }

            var dir = Path.GetDirectoryName(path);

            using (var stream = File.OpenRead(path))
            {
                var sig = Convert.ToBase64String(sha.ComputeHash(stream));
                Data.SetStringValue(vendorConfig.DataObjectToken, new ChangeStringValueToKeyModel { Id = dObjId, Key = key, ProductId = vendorConfig.ProductId, StringValue = sig });
                var cert = Key.Activate(vendorConfig.ActivateToken, vendorConfig.ProductId, key, Helpers.GetMachineCodePI());
                var certName = Path.Combine(dir, Path.GetFileName(path) + ".skm");
                File.WriteAllText(certName, JsonConvert.SerializeObject(cert));

                MessageBox.Show($"Assembly {path} was successfully signed.");
            }
        }
    }
}
