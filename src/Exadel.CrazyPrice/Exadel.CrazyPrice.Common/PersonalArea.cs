using System.Collections.Generic;

namespace Exadel.CrazyPrice.Common
{
    public class PersonalArea
    {
        public Person Person { get; set; }

        public IEnumerable<ShortDiscountProgram> DiscountPrograms { get; set; }
    }
}
