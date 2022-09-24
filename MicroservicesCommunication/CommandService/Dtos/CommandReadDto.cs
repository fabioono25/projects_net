﻿using System;
using CommandService.Models;
using System.ComponentModel.DataAnnotations;

namespace CommandService.Dtos
{
    public class CommandReadDto
    {
        public int Id { get; set; }

        public string HowTo { get; set; }

        public string CommandLine { get; set; }

        public int PlatformId { get; set; }
    }
}

