using System.Collections.Generic;
using FavFighters.Models;

namespace FavFighters.Services
{
    public class FakeFightersService
    {
        static FakeFightersService _instance;

        public static FakeFightersService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FakeFightersService();

                return _instance;
            }
        }

        public IEnumerable<Fighter> GetFighters()
        {
            return new List<Fighter>
            {
                new Fighter { Tier = "D TIER", Name = "Villager", Category = "sheet", Image = "villager.png" },
                new Fighter { Tier = "C TIER", Name = "Lucas", Category = "sheet", Image = "lucas.png" },
                new Fighter { Tier = "E TIER", Name = "Jiggipluff", Category = "pokeball", Image = "jigglypuff.png" },
                new Fighter { Tier = "D TIER", Name = "Rosalina", Category = "mushroom", Image = "rosalina.png" },
                new Fighter { Tier = "E TIER", Name = "King Dedede", Category = "mushroom", Image = "kingdedede.png" },
                new Fighter { Tier = "B TIER", Name = "Bowser", Category = "mushroom", Image = "bowser.png" }
            };
        }
    }
}