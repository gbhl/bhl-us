using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Web.Mvc;

namespace MOBOT.BHL.Web2.Models
{
    public class ContactModel
    {
        public string Foo { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int? Subject { get; set; } = 22;
        public string Comment { get; set; }
        public string srOCLC { get; set; }
        public string srTitle { get; set; }
        public string srYear { get; set; }
        public string srType { get; set; } = "Book";
        public string srVolume { get; set; } = "All Volumes";
        public string srEdition { get; set; }
        public string srISBN { get; set; }
        public string srISSN { get; set; }
        public string srAuthor { get; set; }
        public string srPublisher { get; set; }
        public string SelectedLanguage { get; set; }
        public string SelectedLanguageName { get; set; }
        public IEnumerable<SelectListItem> LanguageList { get; set; }
        public string srNote { get; set; }
        public bool Submitted { get; set; } = false;
        public string ConfirmationSubject { get; set; }
        public string ConfirmationText { get; set; }
        public string ErrorText { get; set; }
        public string FeedbackRefererURL { get; set; }

        public ContactModel()
        {

        }
    }
}