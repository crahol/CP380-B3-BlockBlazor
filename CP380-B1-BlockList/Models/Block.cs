using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;

namespace CP380_B1_BlockList.Models
{
    public class Block
    {
        public int Nonce { get; set; }
        public DateTime TimeStamp { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public List<Payload> Data { get; set; }

        public Block(DateTime timeStamp, string previousHash, List<Payload> data)
        {
            Nonce = 0;
            TimeStamp = timeStamp;
            PreviousHash = previousHash;
            Data = data;
            Hash = CalculateHash();
        }

        //
        // JSON serialisation:
        //   https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to?pivots=dotnet-5-0
        //
        public string CalculateHash()
        {
            var sha256 = SHA256.Create();
            var json = JsonSerializer.Serialize(Data);

            //
            // TODO - Done
            //                
            var inputStr = $"{this.TimeStamp:yyyy-MM-dd h:mm:ss tt}-{this.PreviousHash}-{this.Nonce}-{json}"; // TODO

            var inputBytes = Encoding.ASCII.GetBytes(inputStr);
            var outputBytes = sha256.ComputeHash(inputBytes);

            return Base64UrlEncoder.Encode(outputBytes);
        }

        public void Mine(int difficulty)
        {
            // TODO - Done
            string hashValue = new('C', difficulty);

            while (Hash.Substring(0, difficulty) != hashValue)
            {
                this.Nonce++;
                Hash = CalculateHash();
            }
        }
    }
}
