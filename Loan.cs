using System;
using System.Collections.Generic;
namespace DP_Specification_Pattern
{

    public class Loan
    {
        protected string _serial, _loanType;
        protected double _initLoanAmount, _newLoanAmount, _initLoanRate, _newLoanRate;

        public string Serial => _serial;
        public string LoanType => _loanType;
        public double InitLoanAmount => _initLoanAmount;
        public double NewLoanAmount => _newLoanAmount;
        public double InitLoanRate => _initLoanRate;
        public double NewLoanRate => _newLoanRate;



        public Loan(string serial, string loanType, double initLoanAmount, double newLoanAmount, double initLoanRate, double newLoanRate) 
        {
            _serial = serial;
            _loanType = loanType;
            _initLoanAmount = initLoanAmount;
            _newLoanAmount = newLoanAmount;
            _initLoanRate = initLoanRate;
            _newLoanRate = newLoanRate;

        }

    }
    
}
