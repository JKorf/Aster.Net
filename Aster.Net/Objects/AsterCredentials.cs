using CryptoExchange.Net.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aster.Net.Objects
{
    public class AsterCredentials : ApiCredentials
    {

        public AsterCredentials(HMACCredential hmacCredential)
            : base(hmacCredential) 
        {
        }

        public AsterCredentials(AsterECDSACredential ecdsaCredential)
            : base(ecdsaCredential)
        {
        }

        public AsterCredentials(HMACCredential? hmacCredential, AsterECDSACredential? ecdsaCredential)
            : base(hmacCredential, ecdsaCredential)
        {
        }

        public override ApiCredentials Copy() => 
            new AsterCredentials(GetCredential<HMACCredential>(), GetCredential<AsterECDSACredential>());
    }
}
