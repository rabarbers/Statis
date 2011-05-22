using System.Collections.Generic;
using System;

namespace StatisServiceContracts
{
    public class StatisticsModule
    {
        private Questionnaire _q { get; set; }
        private FilledQuestionnaire _fq { get; set; }
		private List<string> _rList;
        public StatisticsModule(Questionnaire q, FilledQuestionnaire fq)
		{
			this._q = q;
            this._fq = fq;
			_rList = new List<string>();
		}
        public void CalcCorrelation(string field1, string field2, double a, double b)
        {
            // actual formula missing
            double value = a + b;
            string result = "The coefficient of correlation between " + field1 + " and " + field2 + " is " + string.Format("0.0000", value);
            _rList.Add(result);
        }

/*        public void CalcDispersion(string field1, string field2, double a, double b)
		{
            // actual formula missings
            double value = a + b;
            string result = "The coefficient of dispersion for " + field1 + " and " + field2 + " is " + string.Format("0.0000", value);
            this._rList.Add(result);
		}*/
        /// many more methods should follow here

        // find average from a range of numbers
        private static double AssistAverage(double[] input)
        {
            int len = input.Length;
            if (len == 0)
                return 0;
            else
            {
                double sum = 0;
                for (int i = 0; i < input.Length; i++)
                    sum += input[i];
                return sum / len;
            }
        }
        public static double AssistVariance(double[] input)
        {
            int len = input.Length;
            // Get average
            double avg = AssistAverage(input);
            double sum = 0;
            for (int i = 0; i < input.Length; i++)
                sum += Math.Pow((input[i] - avg), 2);
            return Math.Sqrt(sum / len);
        }
        public static double AssistStdDeviation(double[] input)
        {
            return Math.Sqrt(AssistVariance(input));
        }
        public static void AssistCorrelation(double[] x, double[] y, ref double covXY, ref double pearson)
        {
            if (x.Length != y.Length)
                throw new Exception("Length of sources is different");
            double avgX = AssistAverage(x);
            double stdevX = AssistStdDeviation(x);
            double avgY = AssistAverage(y);
            double stdevY = AssistStdDeviation(y);
            int len = x.Length;

            for (int i = 0; i < len; i++)
                covXY += (x[i] - avgX) * (y[i] - avgY);
            covXY /= len;
            pearson = covXY / (stdevX * stdevY);
        }
    }
}