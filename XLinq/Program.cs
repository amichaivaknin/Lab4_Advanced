
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace XLinq
{
    class Program
    {
        static void Main()
        {
            var xlmSystem = typeof(Assembly).Assembly.GetExportedTypes()
                .Where(x => x.IsClass)
                .Select(@class =>
                new XElement("Type",
                new XAttribute("FullName", @class.FullName),
                    new XElement("Propirties",
                        @class.GetProperties().Select(p =>
                            new XElement("Property",
                            new XAttribute("Name", p.Name),
                            new XAttribute("Type", p.PropertyType.FullName ?? "T")))),
                    new XElement("Methodes",
                        @class.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                        .Where(method => !method.IsSpecialName)
                        .Select(method =>
                    new XElement("Method",
                    new XAttribute("Name", method.Name),
                    new XAttribute("ReturnType", method.ReturnType.FullName ?? "T"),
                        new XElement("Parameter",
                            method.GetParameters().Select(parameter =>
                        new XElement("Parameter",
                        new XAttribute("Name", parameter.Name),
                        new XAttribute("Type", parameter.ParameterType)))))))));

            var xmltypes = new XElement("xlmSystem", xlmSystem);
            // Console.WriteLine(xmltypes);     

            var xElements = xlmSystem as XElement[] ?? xlmSystem.ToArray();
            var noPropertiesList =
                xElements.Select(type => new {type, element = type.Element("Propirties")})
                    .Where(@t => @t.element != null && !@t.element.Descendants().Any())
                    .Select(@t => new {@t, nameOfType = @t.type.Attribute("FullName").ToString()})
                    .OrderBy(@t => @t.nameOfType)
                    .Select(@t => @t.nameOfType);

            //Console.WriteLine($"All types that have no properties {noPropertiesList.Count()} : ");

            //Console.WriteLine($"Total number of methods, not including inherited ones: {xElements.Sum(t => t.Descendants("Method").Count())}");

            var statistics =
                     xElements.Descendants("Parameter")
                    .GroupBy(element => (string) element.Attribute("Type"))
                    .OrderByDescending(grp => grp.Count())
                    .Select(grp => new {Name = grp.Key, Count = grp.Count()});

            //  Console.WriteLine($"Most Common type: {statistics.First().Name} namber of properties :{statistics.First().Count}");

            var orderdByMethods =
                xElements.Select(type => new {type, methods = type.Descendants("Method").Count()})
                    .OrderByDescending(@t => @t.methods)
                    .Select(@t => new
                    {
                        Name = (string) @t.type.Attribute("FullName"),
                        Methods = @t.methods,
                        Properties = @t.type.Descendants("Property").Count()
                    });

            //foreach (var order in orderdByMethods)
            //{
            //    Console.WriteLine(order);
            //}

            var types = xElements.OrderBy(type => (string) type.Attribute("FullName"))
                .GroupBy(type => type.Descendants("Method").Count(), type => new
                {
                    Name = (string) type.Attribute("FullName"),
                    Methods = type.Descendants("Method").Count(),
                    Properties = type.Descendants("Property").Count()
                }).OrderByDescending(grp => grp.Key);

            //foreach (var group in types)
            //{
            //    foreach (var type in group)
            //    {
            //        Console.WriteLine(type);
            //    }
            //    Console.WriteLine();
            //}
        }
    }
}
