using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Demo.CustomJsonConverter.KeyValue.Validators;
using Demo.Utilities.AssemblyUtilities;
using Demo.Utilities.CharUtilities;
using Demo.Utilities.PropertyUtilities;

namespace Demo.CustomJsonConverter.KeyValue
{
    public class KeyValueCreator
    {
        // The returned list has all the key value pairs for the object that we will create
        public static List<KeyValuePair<string, string>> CreateKeyValueList
            (string json,Type  type)
        {
            List<KeyValuePair<string, string>> list = new();

            #region typeNames Explanation
            //First keep in mind that in the keyvalue list I keep the keys as following:
            //Person.Id or Address.AddressLine so TypeName.PropertyName
            //So I use Type names for the keys so that
            //the properties of type A are different from properties type B
            //because we can have a following case where we have the same named keys-->
            //"{id:3,leader:{id:3,name:'Vahid'} }" there are 2 the same named keys (id and id) 
            //We need to make each property for each object unique so that there is no conflict
            //that's why I keep track of typeNames 
            //When I encounter the char '}' which means it is the end of the object
            //I pop a type from the stack and it returns to the previous type
            //Stack in this case is the best data structure 
            #endregion
            Stack<string> typeNames = new();

            json = json.Trim();

            //Remove the wrapper curly braces --> '{' and '}'
            json = json.Trim().Remove(json.Length - 1, 1);
            json = json.Remove(0, 1);

            //Splitting our json by the comma seperator
            string[] items = json.Trim().Split(",");

            //Push the first type to the stack
            typeNames.Push(type.Name);

            Type[] allTypes = AssemblyGetter.GetAssembly().GetTypes();
            PropertyInfo prop; 
            foreach (var item in items)
            {
                #region keyvalue Explanation
                //Splitting our items array by the semi-colon seperator to 
                //separate elements (such as keys on the left and values on the right)
                #endregion
                string[] keyvalue = item.Trim().Split(":");

                string key = "";
                string value = "";
                #region if statement Explanation
                //if the keyvalue array has just 2 elements then it means 
                //we have keys on index zero and values on index 1
                #endregion
                if (keyvalue.Length == 2)
                {
                    key = QuoteRemover.RemoveQuotes(keyvalue[0].Trim());
                    #region Explanation
                    //check if the property of the type with this key exist
                    //if no then ignore everything and
                    //we don't add anything to the key value list
                    #endregion
                    if (!PropertyValidator.PropertyExist(allTypes, typeNames, key))
                    {
                        continue;
                    }
                    //if it exists then get the property
                    prop = PropertyGetter.GetProperty(allTypes, typeNames, key);

                    value = keyvalue[1].Trim();
                    if (ValueValidator.WrapperQuotesExist(value))
                    {
                        value = value.RemoveFirtsAndLastChar();
                    }
                    if (value.Last() == '}')
                    {
                        value = value.Remove(value.Length-1, 1);
                        list.Add(new KeyValuePair<string, string>
                        (typeNames.Peek() + '.' + prop.Name, value));
                        //Since it is the last element, we get to the previous type
                        typeNames.Pop();
                        continue;
                    }
                    list.Add(new KeyValuePair<string, string>
                        (typeNames.Peek()+ '.' + prop.Name, value));
                    continue;
                }
                #region Explanation
                // If the length is 3 then it means we encountered an object
                //In this case in the keyvalue array we get 3 elements 
                //for example { name:'Lala',Company:{name'Kibrit',...} }
                //on index 0 we have the type name ( 'Company' )
                //on index 1 our key with the '{' char
                //at the beginning of our string ( '{name' )
                //on index 2 we have our value ( 'Kibrit' )
                #endregion
                else if (keyvalue.Length == 3)
                {
                    key = QuoteRemover.RemoveQuotes(keyvalue[1]).Trim();
                    key=key.Remove(0, 1).Trim();
                    typeNames.Push(keyvalue[0]);
                    value = keyvalue[2].Trim();
                    #region Explanation
                    // the object has just one property for example -->
                    // { name:'Lala',Company:{name'Kibrit'} }
                    // then on index 2 of keyvalue array we will have 
                    // our value with extra closing curly brackets that we need to exclude
                    // we will have something like this --> ( 'Kibrit}}' )
                    //so we need to first go to the previous type
                    //(pop the stack) as many times as the number
                    //of closing curly brackets is (in this case 2 times) 
                    #endregion
                    if (value.Last()=='}')
                    {
                        foreach(char character in value)
                        {
                            value = value.Remove(value.Length - 1, 1);
                            typeNames.Pop();
                            if (value.Last()!='}') break;
                        }
                    }
                    if (ValueValidator.WrapperQuotesExist(value))
                    {
                        value = value.RemoveFirtsAndLastChar();
                    }
                    if (!PropertyValidator.PropertyExist(allTypes, typeNames, key))
                    {
                        continue;
                    }
                    prop = PropertyGetter.GetProperty(allTypes, typeNames, key);
                    list.Add(new KeyValuePair<string, string>
                        (typeNames.Peek()+ '.' + prop.Name, value));
                }
            }
            return list;
        }
    }
}
