using ArtNews.Models;
using System.Collections.Generic;

namespace ArtNews.Services
{
    public class ArtService
    {
        private static ArtService _instance;

        public static ArtService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ArtService();
                return _instance;
            }
        }

        public Author GetAuthorInfo()
        {
            return new Author
            {
                Name = "Sandro Botticelli",
                Dates = "March 1, 1445 - May 17, 1510",
                Description = "Candelo Botticelli, formerly known as Alexandro Felipe, was a Florentine artist in the early Renaissance.",
                Highlights = GetArtItems()
            };
        }

        private List<ArtItem> GetArtItems()
        {
            return new List<ArtItem>
                {
                    new ArtItem
                    {
                        Name = "The birth of Venus",
                        Number = 1,
                        NumberText ="NO.1",
                        Author = "Sandro Botticelli",
                        Banner = "botticelli01.jpg",
                        BriefIntroducction = "The Birth of Venus (Italian: Nascita di Venere [ˈnaʃʃita di ˈvɛːnere]) is a painting by the Italian artist Sandro Botticelli probably made in the mid 1480s. It depicts the goddess Venus arriving at the shore after her birth, when she had emerged from the sea fully-grown (called Venus Anadyomene and often depicted in art). The painting is in the Uffizi Gallery in Florence, Italy.",
                        ArtAppreciation = "The painting is large, but slightly smaller than the Primavera, and where that is a panel painting, this is on the cheaper support of canvas. Canvas was increasing in popularity, perhaps especially for secular paintings for country villas, which were decorated more simply, cheaply and cheerfully than those for city palazzi, being designed for pleasure more than ostentatious entertainment.",
                        Related = GetRelatedArtItems()
                    },
                    new ArtItem
                    {
                        Name = "Primavera",
                        Number = 2,
                        NumberText ="NO.2",
                        Author = "Sandro Botticelli",
                        Banner = "botticelli02.jpg",
                        BriefIntroducction = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        ArtAppreciation = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        Related = GetRelatedArtItems()
                    },
                    new ArtItem
                    {
                        Name = "The adoration of the magi",
                        Number = 3,
                        NumberText ="NO.3",
                        Author = "Sandro Botticelli",
                        Banner = "botticelli03.jpg",
                        BriefIntroducction = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        ArtAppreciation = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        Related = GetRelatedArtItems()
                    },
                    new ArtItem
                    {
                        Name = "Venus and Mars",
                        Number = 4,
                        NumberText ="NO.4",
                        Banner = "botticelli04.jpg",
                        BriefIntroducction = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        ArtAppreciation = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        Related = GetRelatedArtItems()
                    },
                    new ArtItem
                    {
                        Name = "The birth of Christ",
                        Number = 5,
                        NumberText ="NO.5",
                        Author = "Sandro Botticelli",
                        Banner = "botticelli05.jpg",
                        BriefIntroducction = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        ArtAppreciation = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        Related = GetRelatedArtItems()
                    },
                    new ArtItem
                    {
                        Name = "La calumnia de Apeles",
                        Number = 6,
                        NumberText ="NO.6",
                        Author = "Sandro Botticelli",
                        Banner = "botticelli06.jpg",
                        BriefIntroducction = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        ArtAppreciation = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        Related = GetRelatedArtItems()
                    },
                    new ArtItem
                    {
                        Name = "Mystic Christmas",
                        Number = 7,
                        NumberText ="NO.7",
                        Author = "Sandro Botticelli",
                        Banner = "botticelli07.jpg",
                        BriefIntroducction = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        ArtAppreciation = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        Related = GetRelatedArtItems()
                    },
                    new ArtItem
                    {
                        Name = "The temptation of Christ",
                        Number = 8,
                        NumberText ="NO.8",
                        Author = "Sandro Botticelli",
                        Banner = "botticelli08.jpg",
                        BriefIntroducction = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        ArtAppreciation = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        Related = GetRelatedArtItems()
                    }
                };
        }

        private List<ArtItem> GetRelatedArtItems()
        {
            return new List<ArtItem>
            {
                 new ArtItem
                    {
                        Name = "Primavera",
                        Number = 2,
                        NumberText ="NO.2",
                        Author = "Sandro Botticelli",
                        Banner = "botticelli02.jpg",
                        BriefIntroducction = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        ArtAppreciation = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."
                    },
                    new ArtItem
                    {
                        Name = "The adoration of the magi",
                        Number = 3,
                        NumberText ="NO.3",
                        Author = "Sandro Botticelli",
                        Banner = "botticelli03.jpg",
                        BriefIntroducction = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        ArtAppreciation = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."
                    },
                    new ArtItem
                    {
                        Name = "Venus and Mars",
                        Number = 4,
                        NumberText ="NO.4",
                        Banner = "botticelli04.jpg",
                        BriefIntroducction = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        ArtAppreciation = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."
                    },
                    new ArtItem
                    {
                        Name = "The birth of Christ",
                        Number = 5,
                        NumberText ="NO.5",
                        Author = "Sandro Botticelli",
                        Banner = "botticelli05.jpg",
                        BriefIntroducction = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        ArtAppreciation = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."
                    }
            };
        }
    }
}
