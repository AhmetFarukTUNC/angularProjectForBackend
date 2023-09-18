using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {

        public static IResult Run(params IResult[] logics) //logics:kurallar
        {
            
            foreach (var logic in logics)
            {
                if (!logic.Success) //kurala uymayanı döndürür.
                {
                    return logic; // kural:logic
                }
            }
            return null;
        }

    }
}
