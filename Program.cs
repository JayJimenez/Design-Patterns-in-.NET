namespace DP_Specification_Pattern
{

    public interface ISpecification<T>
    {
        bool IsSatisfied(Loan l);

    }

    public interface IFilter<T>
    {
        IEnumerable<T> Process(IEnumerable<T> input, ISpecification<T> spec);
    }

    public class AmountSpecification : ISpecification<Loan>
    {
        private readonly int maxDifference = 3000;
        public bool IsSatisfied(Loan l)
        {
            return Math.Abs(l.InitLoanAmount - l.NewLoanAmount) <= maxDifference;
        }
    }

    public class RateSpecification : ISpecification<Loan>
    {
        private readonly double maxDifference = .3;

        public bool IsSatisfied(Loan l)
        {
            return Math.Abs((double)l.NewLoanRate - l.InitLoanRate) <= maxDifference;
        }
    }

    public class AndSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification<T> first;
        private readonly ISpecification<T> second;

        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            this.first = first ?? throw new ArgumentNullException(paramName: nameof(first));
            this.second = second ?? throw new ArgumentNullException(paramName: nameof(second));
        }

        public bool IsSatisfied(Loan l)
        {
            return first.IsSatisfied(l) && second.IsSatisfied(l);
        }
    }

    public class Filter : IFilter<Loan>
    {
        public IEnumerable<Loan> Process(IEnumerable<Loan> loans, ISpecification<Loan> spec)
        {
            foreach (var loan in loans)
            {
                if (spec.IsSatisfied(loan))
                {
                    yield return loan;
                }
            }
        }
    }


    public class Demo
    {
        static void Main(string[] args)
        {
            Loan loan1 = new Loan("130498", "Auto", 43000, 45000, 8.2, 8.3); // approved since amount diff is <= 3000
            Loan loan2 = new Loan("534579", "Auto", 27000, 27000, 4.3, 5.6); // NOT approved since rate diff >= .3
            Loan loan3 = new Loan("73445679", "Auto", 17000, 29000, 1.3, 11.2); // NOT approved since amount diff >= 3000
            Loan loan4 = new Loan("434356", "Auto", 87000, 87000, 1.3, 1.2); // should be approved since rate diff <= .3
            Loan loan5 = new Loan("8345356", "Auto", 37000, 37000, 5.3, 5.6); // should be approved since rate diff == .3
            Loan loan6 = new Loan("13457786", "Auto", 57000, 58000, 3.3, 3.3); // should be approved since amount diff <= 3000

            Loan[] loansToProcess = { loan1, loan2, loan3, loan4, loan5, loan6 };

            Filter filter = new();

            foreach(var l in filter.Process(loansToProcess, 
                new AndSpecification<Loan>(new AmountSpecification(), new RateSpecification() ))) 
            {
                Console.WriteLine("Approved Loans: " + l.Serial);
            }

        }

    }
}
