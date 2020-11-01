using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatten.StructureDesignPattens
{
    class Proxy : IDesignPatten
    {
        public void Run()
        {
            SaftyPasswordProvider passwordProvider = new SaftyPasswordProvider(new OrgProvider());

            Console.WriteLine("token is 30");
            passwordProvider.GetAccess(30);
            Console.WriteLine("password is "+passwordProvider.GetPassword());

            //正确的token
            Console.WriteLine("token is 2020");
            passwordProvider.GetAccess(2020);
            Console.WriteLine("password is " + passwordProvider.GetPassword());
        }

        interface IPasswordProvider
        {
            string GetPassword();
        }

        class OrgProvider:IPasswordProvider
        {
            public string GetPassword() => "ABCDEFG";
        }
        class SaftyPasswordProvider:IPasswordProvider
        {
            private bool HasAccess=false;

            OrgProvider provider;

            public SaftyPasswordProvider(OrgProvider provider)
            {
                this.provider = provider;
            }

            public void GetAccess(int token)
            {
                if (token == 2020)
                {
                    HasAccess = true;
                    Console.WriteLine("correct token");
                }
                    
                else
                {
                    HasAccess = false;
                    Console.WriteLine("wrong token");
                }
                    
            }

            public string GetPassword()
            {
                if (HasAccess)
                {
                    return provider.GetPassword();
                }
                Console.WriteLine("No access.");
                return null;
            }
        }
    }
}
