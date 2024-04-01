<<<<<<< HEAD
﻿using Microsoft.EntityFrameworkCore;
=======
﻿using DateTime = System.DateTime;

using Microsoft.EntityFrameworkCore;
>>>>>>> 682fefab4433ca557e060486ef92392605be8619

namespace EventApi.Models
{
    public partial class Token
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string TokenHash { get; set; }

        public string TokenSalt { get; set; }

        public DateTime Timestamp { get; set; }

        public DateTime ExpiryDate { get; set; }

        public virtual User User { get; set; }
    }
}
