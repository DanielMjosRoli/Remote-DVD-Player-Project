using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography.X509Certificates;
using test1;

namespace test1{
    public class Test1
    {
        public string name;
        public int age;

        public Test1(string name, int age)
        {
            this.name = name;
            this.age = age;
        }
        public static void Main(string[] args)
        {
            Test1 test = new("Daniel", 25);
            Console.WriteLine(test.name + ' ' + test.age.ToString());
        }
    }
}

