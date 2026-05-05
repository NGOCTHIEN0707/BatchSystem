using Domain.Logins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Domain.Logins.StaffCodes
{
    public class StaffCode
    {
        public string StaffCodeId { get; private set; }

        public int Code { get; private set; }   // mã số dạng 221356

        public ERole Role { get; private set; } // manager hay staff

        public bool IsUsed { get; private set; } =false;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public StaffCode()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public StaffCode(int code, ERole role, bool isUsed)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Code = code;
            Role = role;
            IsUsed = false;
        }
        public void MarkAsUsed()
        {
            IsUsed = true;
        }
    }
}
