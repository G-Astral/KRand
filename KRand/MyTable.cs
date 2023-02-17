using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRand
{
    public static class MyTable
    {
        public static DataTable ToDataTable<T>(this T[,] matrix)
        {
            var res = new DataTable();

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                res.Columns.Add("col" + i, typeof(T));
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                var row = res.NewRow();

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    row[j] = matrix[i, j];
                }

                res.Rows.Add(row);
            }

            return res;
        }
    }
}
