using RazorDropDown.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorDropDown.Domain;
using RazorDropDown.Services;
using static System.Formats.Asn1.AsnWriter;

namespace DropDown.Models
{

    public class SelectViewModel
    {
        public string SelectId { get; set; }
        public IEnumerable<SelectListItem> List { get; set; }

        public static List<SelectListItem> BuildList(List<EventTypeDTO> eventTypes)
        {
            List<SelectListItem> items = eventTypes.Select( s => new SelectListItem
            {
                 Text = s.Title,
                  Value = s.Id
            }).ToList();
            return items;
        }

        internal static IEnumerable<SelectListItem> BuildList()
        {
            throw new NotImplementedException();
        }
    }

    public class GradeStoreVM
    {
        public string GradeId { get; set; }
        public IEnumerable<SelectListItem> List { get; set; }
        public List<Shop> GradeStores { get; set; }

        public static List<SelectListItem> BuildList(List<Grade> lst)
        {
      
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var i in lst)
            {
                items.Add(new SelectListItem { Text = i.Description, Value = i.GradeId.ToString() });
            }

            return items;
        }

    }

    public class EventTypeVM
    {
        public string EventType { get; set; }
    }

    public class EventListVM
    {
        public string Name { get; set; }
        public string EventTypeId { get; set; }
        public IEnumerable<SelectListItem> List { get; set; }

        public static List<SelectListItem> BuildList(List<EventTypeDTO> lst)
        {

            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var i in lst)
            {
                items.Add(new SelectListItem { Text = i.Title, Value = i.Id });
            }

            return items;
        }

    }


}
