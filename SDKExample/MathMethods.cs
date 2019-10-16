using System;
using System.Collections.Generic;
using System.Reflection;

namespace SDKExample
{
    public class MathMethods
    {
        private Dictionary<string, bool> permittedMethods = new Dictionary<string, bool>();

        public MathMethods(string licenseKey)
        {


        }

        public bool Status()
        {
            string RSA = "<RSAKeyValue><Modulus>sGbvxwdlDbqFXOMlVUnAF5ew0t0WpPW7rFpI5jHQOFkht/326dvh7t74RYeMpjy357NljouhpTLA3a6idnn4j6c3jmPWBkjZndGsPL4Bqm+fwE48nKpGPjkj4q/yzT4tHXBTyvaBjA8bVoCTnu+LiC4XEaLZRThGzIn5KQXKCigg6tQRy0GXE13XYFVz/x1mjFbT9/7dS8p85n8BuwlY5JvuBIQkKhuCNFfrUxBWyu87CFnXWjIupCD2VO/GbxaCvzrRjLZjAngLCMtZbYBALksqGPgTUN7ZM24XbPWyLtKPaXF2i4XRR9u6eTj5BfnLbKAU5PIVfjIS+vNYYogteQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            return SKM.V3.Methods.Helpers.VerifySDKLicenseCertificate(Assembly.GetCallingAssembly(),RSA, null, null);
        }

        //private bool licenseCheck(string licenseKey)
        //{
 
        //        // everything went fine if we are here!
        //        permittedMethods = new Dictionary<string, bool>();

        //        if (result.LicenseKey.F5)
        //        {
        //            permittedMethods.Add("Abs", true);
        //        }
        //        if (result.LicenseKey.F6)
        //        {
        //            permittedMethods.Add("Factorial", true);
        //        }
        //        if (result.LicenseKey.F7)
        //        {
        //            permittedMethods.Add("Fibonacci", true);
        //        }

          

        //}

        /// <summary>
        /// Compute the absolute value of a number
        /// </summary>
        public int Abs(int num)
        {
            if (!permittedMethods.ContainsKey("Abs"))
                throw new ArgumentException("The use of this method is not permitted.");

            if (num > 0)
                return num;
            return -num;
        }

        /// <summary>
        /// Compute the factorial, eg. num!.
        /// </summary>
        public int Factorial(int num)
        {
            if (!permittedMethods.ContainsKey("Factorial"))
                throw new ArgumentException("The use of this method is not permitted.");

            if (num == 0)
                return 1;
            return num * Factorial(num - 1);
        }

        /// <summary>
        /// Compute the nth fibonacci number.
        /// </summary>
        public int Fibonacci(int num)
        {
            if (!permittedMethods.ContainsKey("Fibonacci"))
                throw new ArgumentException("The use of this method is not permitted.");

            return fibonacciHelper(num);
        }

        // we use a helper methods to avoid checking the permission on each iteration.
        private int fibonacciHelper(int num)
        {
            if (num == 0 || num == 1)
                return 1;
            return fibonacciHelper(num - 1) + fibonacciHelper(num - 2);
        }
    }
}
