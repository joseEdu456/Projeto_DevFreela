﻿using DevFreela.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.StartProject
{
    public class StartProjectCommand : IRequest<ResultViewModel>
    {
        public int Id { get; set; }

        public StartProjectCommand(int id)
        {
            Id = id;
        }
    }
}
