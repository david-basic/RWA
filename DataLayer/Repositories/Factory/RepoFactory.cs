using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Factory
{
    public class RepoFactory
    {
        private static Repo repo;
        public static Repo GetRepo()
        {
            if (repo is null)
            {
                repo = new Repo();
            }

            return repo;
        }
    }
}
