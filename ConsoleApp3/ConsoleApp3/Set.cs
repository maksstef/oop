using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public class Set
    {
        private List<int> _collection;//create field type List


        public List<int> Collection { get => _collection; set => _collection = value; }//properties

        //constructor for initialyze _collection
        public Set()
        {
            _collection = new List<int>();
            Date date = new Date();
        }
        //method for overloading operator
        public static Set operator >>(Set obj,int item)
        {
            obj.Collection.Remove(item);
            return obj;
        }

        public void ShowData()
        {
            foreach (var item in Collection)
            {
                Console.WriteLine(item);
            }
        }

        public static Set operator <<(Set obj,int item)
        {
            obj.Collection.Add(item);
            return obj;
        }

        public static bool operator <(Set obj, int item)
        {
            return true;
        }

        public static bool operator >(Set obj, int item)
        {

            return obj.Collection.Contains(item);
        }

        public static bool operator !=(Set obj,Set obj2)
        {
            return obj.Equals(obj2);
        }

        public static bool operator ==(Set obj,Set obj2)
        {
            return true;
        }

        public static List<int> operator %(Set obj, Set obj2)
        {
            var result = new List<int>();
            foreach (var item in obj.Collection)
            {
                if (obj2>item)
                {
                    result.Add(item);
                }
            }

            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
            return result;
            
        }

        public void FindMin()
        {
            Console.WriteLine(Collection.Min()); 
        }

        public void SortBy()
        {
            Collection.Sort();
        }

        public class Date
        {
            private DateTime _recentDate;

            public DateTime RecentDate { get => _recentDate; set => _recentDate = value; }

            public Date()
            {
                this._recentDate = DateTime.Now;
            }

        }

    }
 
    public class Owner
    {

        private int id;

        private string name;

        private string organisation;


        //for initialyse on input
        public Owner(int id, string name, string organisation)
        {

            this.Id = id;

            this.Name = name;

            this.Organisation = organisation;

        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Organisation { get => organisation; set => organisation = value; }
    }

    public static class MathOperation
    {
        public static void FindMax(Set obj)
        {
            Console.WriteLine(obj.Collection.Max());
        }

        public static void FindMin(Set obj)
        {
            Console.WriteLine(obj.Collection.Min());
        }

        public static void FindCount(Set obj)
        {
            Console.WriteLine(obj.Collection.Count());
        }

        public static void Extension(this Set obj, int itemToFind)
        {
            if (obj.Collection.Contains(itemToFind))
            {
                Console.WriteLine($"Given set contains item {itemToFind}");
            }
            else
            {
                Console.WriteLine($"Doesn't contain given item {itemToFind}");
            }
            
        }
    }

    }
