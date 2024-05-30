using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryOasis.DB
{
    internal class DBConnection
    {
        public static CulinaryOasisEntities4 culinaryOasis = new CulinaryOasisEntities4();

        public static Dish selectedDish;
    }
}
