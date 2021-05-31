using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pitney.Models
{
    public static class SaveReadToFile
    {
        public static int GetLastIDNumber()
        {
            int lastID;
            try
            {
                int count;
                try
                {
                  count = File.ReadAllLines("AddressBook.txt").Length;
                }
                catch (IOException e)
                {
                    return 0;
                }
                if(count == 0)
                {
                    return 0;
                }                
                string lastLine = File.ReadLines("AddressBook.txt").Last();
                bool success = Int32.TryParse(lastLine, out lastID);
                if(success)
                {
                    return lastID;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                throw new Exception();
            }
        }
       public static bool SaveAddressToBook(AddressBook addressBook)
       {
            try
            {
                int id = GetLastIDNumber() + 1;
                using StreamWriter file = new("AddressBook.txt", append: true);
                file.WriteLine(addressBook.Country);
                file.WriteLine(addressBook.City);
                file.WriteLine(addressBook.Street);
                file.WriteLine(id);
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static bool DeleteAddressByIdNumber(int id)
        {
            if(GetLastIDNumber() < id)
            {
                throw new Exception("There is no id like " + id);
            }
            try
            { 
                List<AddressBook> addressBooks = new List<AddressBook>();
                addressBooks = ReadAddressFromBook();
                addressBooks.RemoveAll(x => x.Id == id);
                DeleteAllFromAddressBook();
                foreach (var item in addressBooks)
                {
                    SaveAddressToBook(item);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return true;

        }

        public static bool DeleteAllFromAddressBook()
        {
            try
            {
                File.Delete("AddressBook.txt");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }
        public static List<AddressBook> ReadAddressFromBook()
        {
            List<AddressBook> addressBooks = new List<AddressBook>();
            try
            {
                int count = File.ReadAllLines("AddressBook.txt").Length;
                using (var sr = new StreamReader("AddressBook.txt"))
                {
                    for (int i = 0; i < count / 4; i++)
                    {
                        string country = sr.ReadLine();
                        string city = sr.ReadLine();
                        string street = sr.ReadLine();
                        int id = Int32.Parse(sr.ReadLine());
                        addressBooks.Add(new AddressBook { Country = country, City = city, Street = street, Id = id });
                    } 
                }
                return addressBooks;
            }
            catch (IOException ex)
            { 
                throw new Exception(ex.Message);
            }
        }
        public static AddressBook ReadLastAddressFromBook()
        {
            int count = File.ReadAllLines("AddressBook.txt").Length;
            if(count == 0)
            {
                throw new Exception("The AddressBook is Empty!");
            }
            else
            {
                try
                {
                    List<string> text = File.ReadLines("AddressBook.txt").Reverse().Take(4).ToList();
                    var addressBooks = (new AddressBook
                    {
                        Country = text[3],
                        City = text[2],
                        Street = text[1],
                        Id = Int32.Parse(text[0])
                    });
                    return addressBooks;
                }
                catch (IOException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
           
        }
    }
}

