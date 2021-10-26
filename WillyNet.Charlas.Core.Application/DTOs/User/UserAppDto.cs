﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillyNet.Charlas.Core.Application.DTOs.User
{
    public class UserAppDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public short Dni { get; set; }
        public bool DeleteLog { get; set; }
        public string Email { get; set; }
        public string ImgUrl { get; set; }
    }
}
