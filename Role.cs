﻿using System.Collections.Generic;

public class Role
{
    public int RoleID { get; set; }
    public string RoleName { get; set; }
    public virtual ICollection<User> Users { get; set; }
}