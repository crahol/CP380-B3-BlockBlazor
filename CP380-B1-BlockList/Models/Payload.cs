
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CP380_B1_BlockList.Models
{
    public enum TransactionTypes
    {
        BUY, SELL, GRANT
    }

    public class Payload
    {
        // TODO
        public string User { get; set; }
        public TransactionTypes TransactionType { get; set; }
        public int Amount { get; set; }
        public string Item { get; set; }
        public Payload(string user, TransactionTypes transactionType, int amount, string item)
        {
            User = user;
            TransactionType = transactionType;
            Amount = amount;
            Item = item;
        }
    }
}
