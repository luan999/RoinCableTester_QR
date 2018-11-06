
using System;

namespace RoinCableTester.Models {
    public class MessageModel {
        public string MachineNo { get; set; }
        public string ProductName { get; set; }
        public string CustomerName { get; set; }
        public int TestTotal { get; set; }
        public string OrderNo { get; set; }
        public string Operator { get; set; }
        public string Validator { get; set; }
        public string Goods { get; set; }
        public string LineBody { get; set; }
        public int ConductingTime { get; set; }
        public int TestResult { get; set; }
        public string TestDesc { get; set; }
        public string TestDate { get; set; }
        public string TestTime { get; set; }
    }
}