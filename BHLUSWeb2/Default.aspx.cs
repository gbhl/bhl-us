using MOBOT.BHL.Web.Utilities;
using System;

namespace MOBOT.BHL.Web2
{
    public partial class _Default : BasePage
    {
        public int homeHeroImage = 1;
        public string homeHeroText = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.bodyID = "body-home";
            Master.harmfulContentBannerVisible = false;
            Master.newFutureBannerVisible = false;

            rssFeed.FeedLocation = AppConfig.ProjectUpdatesFeedLocation;

            // Generate a random number betwee 1 - 9 to be used for home hero image
            Random rand = new Random();
            homeHeroImage = rand.Next(1, 9);
            homeHeroText = getAltText(homeHeroImage);
        }

        protected void btnSearchSubmit_Click(object sender, EventArgs e)
        {
            string searchType = (rdoSearchTypeF.Checked) ? rdoSearchTypeF.Value : rdoSearchTypeC.Value;
            Response.Redirect(string.Format("~/search?searchTerm={0}&stype={1}", Server.UrlEncode(tbSearchTerm.Text), Server.UrlEncode(searchType)));
        }

        private string getAltText(int index)
        {
            string[] altTextList = 
            { 
                "Dictionnaire universel d'histoire naturelle :.Paris :Chez les editeurs MM. Renard, Martinet et cie, rue et Hotel Mignon, 2 (quartier de l'École-de-Médecine) ; et chez Langlois et Leclercq, rue de la Harpe, 81 ; Victor Masson, Place de l'Ecole-de-Médecin1847-1849.",
                "Trattatello popolare sui funghi /. Pavia :premiata tipografia fratelli Fusi,1887.",
                "The quadrupeds of North America,. New York,V.G. Audubon,1851-54..",
                "Illustrations of Himalayan plants :. London :L. Reeve,1855..",
                "Dictionnaire universel d'histoire naturelle :. Paris :Chez les editeurs MM. Renard, Martinet et cie, rue et Hotel Mignon, 2 (quartier de l'École-de-Médecine) ; et chez Langlois et Leclercq, rue de la Harpe, 81 ; Victor Masson, Place de l'Ecole-de-Médecin1847-1849.",
                "Blue-backed Manakin, Chiroxiphia pareola from 'A selection of the birds of Brazil and Mexico : the drawings' London :H.G. Bohn,1841.",
                "A monograph of the Trochilidæ, or family of humming-birds /. London :Printed by Taylor and Francis ;1861 [i.e. 1849-1861].",
                "Description des reptiles nouveaux ou imparfaitement connus de la collection du Muséum d'histoire naturelle et remarques sur la classification et les caractères des reptiles..[Paris :Muséum d'histoire naturelle]1852-."
            };

            if (index < altTextList.Length + 1 )
            {
                return altTextList[index-1];
            }

            return ""; 
        }
    }
}
