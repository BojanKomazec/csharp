using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo
{
    class LinqDemo
    {
        public class SomeObject
        {
            public string Name { get; set; }
            public int Id { get; set; }
            public List<KeyPair> ValuePairs {get;set;} 
        }

        public class KeyPair
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }

        public static void Demo()
        {
            var linqDemo = new LinqDemo();
            //linqDemo.EnumerableRangeDemo();
            //linqDemo.SelectDemo();
            //linqDemo.OrderByDemo();
            //linqDemo.WhereDemo_Predicate1();
            //linqDemo.WhereDemo_Predicate2();
            //linqDemo.SelectManyDemo();
            //linqDemo.DistinctDemo();
            linqDemo.GroupByDemo();
        }

        public void EnumerableRangeDemo()
        {
            Console.WriteLine("LinqDemo.EnumerableRangeDemo()");

            var startingNumber = 7;
            var sequenceLength = 10;
            var seq = Enumerable.Range(startingNumber, sequenceLength);

            // Although Enumerable.Range uses deferred execution (lazy evaluation)
            // its arguments are copied - they are not captured like in lambdas.
            startingNumber = 15;

            foreach(var element in seq)
                Console.WriteLine(element);
        }

        // Select - Projects each element of a sequence into a new form.
        public void SelectDemo()
        {
            Console.WriteLine("LinqDemo.SelectDemo()");

            // create a sequence of square powers of numbers 1 to 10
            var seq = Enumerable.Range(1, 10).Select(n => n * n);

            foreach(var element in seq)
                Console.WriteLine(element);

            // Select can create sequence of another type
            var seq2 = Enumerable.Range(1, 10).Select(n => "Number" + n.ToString());

            foreach(var element in seq2)
                Console.WriteLine(element);

            // select can create sequence of anonymous type objects
            var seq3 = Enumerable.Range(1, 10).Select(n => new { IntValue = n, StrValue = "Number" + n.ToString()});

            foreach(var element in seq3)
                Console.WriteLine(element);            
        }

        // SelectMany: Projects each element of a sequence to an IEnumerable<T> and flattens the resulting sequences into one sequence
        public void SelectManyDemo()
        {
            Console.WriteLine("LinqDemo.SelectManyDemo()");

            var catalogue = new[] {
                new { 
                    Author = "A.B", 
                    Books = new string[] {"Book1", "Book2"}
                },
                new {
                    Author = "C.D.",
                    Books = new string[] {"Book3", "Book4"}
                }
            };

            // List all books in catalogue
            var books = catalogue.SelectMany(item => item.Books);
            books.Dump();

            // List numbers 10, 11, 12, 20, 21, 22,...100, 101, 102
            var seq = Enumerable.Range(1, 10).SelectMany(n => Enumerable.Range(n * 10, 3));
            seq.Dump();
        }

        public void WhereDemo_Predicate1()
        {
            Console.WriteLine("LinqDemo.WhereDemo_Predicate1()");

            List<string> fruits = new List<string> { 
                "apple", "passionfruit", "banana", "mango", "orange", "blueberry", "grape", "strawberry" 
            };

            // Find those strings whose length is < 6
            var res = fruits.Where(s => s.Length < 6);

            foreach(var s in res)
                Console.WriteLine(s);
        }


        // Where: Filters a sequence of values based on a predicate. 
        // Predicate function can take either element or element and its index as arguments.
        // In the second case each element's index is used in the logic of the predicate function.
        public void WhereDemo_Predicate2()
        {
            Console.WriteLine("LinqDemo.WhereDemo_Predicate2()");

            int[] numbers = { 0, 30, 20, 15, 90, 85, 40, 75 };

            // find all elements which are smaller or equal to the number which is their index multiplied by 10
            var res = numbers.Where((element, index) => element <= index * 10);

            foreach(var n in res)
                Console.WriteLine(n);
        }

        // OrderBy - Orders the sequence by the key. 
        // keySelector - function which takes element of the input sequence and returns key.
        // OrderBy uses a version of QuickSort internally.
        public void OrderByDemo()
        {
            Console.WriteLine("LinqDemo.OrderByDemo()");

            var inventory = new[] {
                new { Product = "Tanqueray", Stock = 7 },
                new { Product = "Bombay Sapphire", Stock = 3 },
                new { Product = "Hendricks", Stock = 23 },
                new { Product = "Gordon's", Stock = 8 },
                new { Product = "Beefeather", Stock = 8 },
                new { Product = "London Dry", Stock = 14 }
            };

            // Order items by the stock
            inventory.OrderBy(item => item.Stock).Dump();

            var arr = Enumerable.Range(1, 100);
            arr.ForEach(n => Console.WriteLine(n));

            var arr2 = arr.OrderBy(
                n => 
                {
                    var guid = Guid.NewGuid();
                    Console.WriteLine($"n = {n}, guid={guid}");
                    return guid;
                }
            );

            // To order a sequence by the values of the elements themselves, specify the identity function for keySelector.
            var arr3 = new int[] { 45, 32, 1, 7, 8, 38, 96, 53, 2, 8, 19 };
            arr3.OrderBy( n => n).Dump();

        }

        public void DistinctDemo()
        {
            var names = new string[] { "Alberto", "Bocaccio", "Leonardo", "Leonardo", "Boticelli", "Armando", null, null };
            var uniqueNames = names.Distinct();
            var c = uniqueNames.Count();
            var d = names.Count();
            var b = names.Distinct().Count() == names.Count();
        }


        // Given list of SomeObject instances, create a list of objects which contain Id and ValuePairs 
        // where all Id values are different and ValuePairs is an aggregate of all original ValuePairs
        // grouped by Id
        public void GroupByDemo()
        {
            var objects = new List<SomeObject>();
            objects = new List<SomeObject>()
            {
                new SomeObject
                {
                    Name = "Random Object 1",
                    Id = 5,
                    ValuePairs= new List<KeyPair>()
                    {
                        new KeyPair
                        {
                            Key = "TestKey1",
                            Value = "TestValue1"
                        },
                        new KeyPair
                        {
                            Key = "TestKey2",
                            Value = "TestValue2"
                        }
                    }
                },
                new SomeObject
                {
                    Name = "Random Object 1",
                    Id = 8,
                    ValuePairs= new List<KeyPair>()
                    {
                        new KeyPair
                        {
                            Key = "TestKey5",
                            Value = "TestValue5"
                        },
                        new KeyPair
                        {
                            Key = "TestKey6",
                            Value = "TestValue6"
                        }
                    }
                },
                new SomeObject
                {
                    Name = "Rando Object 2",
                    Id = 5,
                    ValuePairs = new List<KeyPair>()
                    {
                        new KeyPair
                        {
                            Key = "TestKey3",
                            Value = "TestValue3"
                        },
                        new KeyPair
                        {
                            Key = "TestKey4",
                            Value = "TestValue4"
                        }
                    }
                }
            };

            var res = objects.Where(o => o.Id == 5).SelectMany(o => o.ValuePairs);
            foreach (var e in res)
                Console.WriteLine();

            var res2 = objects.GroupBy(o => o.Id);//.Where(group => group.Key == 5);
            Console.WriteLine($"Type = {res2.GetType()}, res3 = {res2}");
            foreach(var group in res2)
            {
                Console.WriteLine($"Key = {group.Key}");
                foreach(var member in group)
                {
                    Console.WriteLine($"    member Name = {member.Name}");
                    //Console.WriteLine($"    member ValuePairs = {member.ValuePairs}");
                }
            }

            var res3 = objects.GroupBy(o => o.Id).Select(group => new { Id = group.Key, ValuePairs = group.SelectMany(g => g.ValuePairs)});
            foreach(var e in res3)
            {
                Console.WriteLine($"Id = {e.Id}");
                foreach(var vp in e.ValuePairs)
                {
                    Console.WriteLine($"    ValuePairs = {vp.Key}, {vp.Value}");
                    //Console.WriteLine($"    member ValuePairs = {member.ValuePairs}");
                }
            }
        }

        public void PrintSequence1<T>(IEnumerable<T> sequence)
        {
            foreach(var element in sequence)
                Console.WriteLine(element);
        }

        // http://stackoverflow.com/questions/800151/why-is-foreach-on-ilistt-and-not-on-ienumerablet
        // http://stackoverflow.com/questions/101265/why-there-is-no-foreach-extension-method-on-ienumerable
        // http://stackoverflow.com/questions/529188/executing-a-certain-action-for-all-elements-in-an-enumerablet
        public void PrintSequence2<T>(IEnumerable<T> sequence)
        {
            sequence.ToList().ForEach(element => Console.WriteLine(element));
        }
    }

    static class MyExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            foreach(var element in sequence)
                action(element);
        }
    }

}