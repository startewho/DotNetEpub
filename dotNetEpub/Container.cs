﻿// Copyright (c) 20011 Oleksandr Tymoshenko <gonzo@bluezbox.com>
// All rights reserved.

// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions
// are met:
// 1. Redistributions of source code must retain the above copyright
//    notice, this list of conditions and the following disclaimer.
// 2. Redistributions in binary form must reproduce the above copyright
//    notice, this list of conditions and the following disclaimer in the
//    documentation and/or other materials provided with the distribution.

// THIS SOFTWARE IS PROVIDED BY THE AUTHOR AND CONTRIBUTORS ``AS IS'' AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
// ARE DISCLAIMED.  IN NO EVENT SHALL THE AUTHOR OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS
// OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
// HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT
// LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY
// OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF
// SUCH DAMAGE.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;


namespace Epub
{
    class Container
    {
        private struct RootFile
        {
            public string file;
            public string mediaType;
        }

        private List<RootFile> _rootFiles;

        internal Container()
        {
            _rootFiles = new List<RootFile>();
        }

        internal void AddRootFile(string file, string mediaType)
        {
            RootFile r;
            r.file = file;
            r.mediaType = mediaType;

            _rootFiles.Add(r);
        }

        internal XElement ToElement()
        {
            XNamespace ns = "urn:oasis:names:tc:opendocument:xmlns:container";
            XElement element = new XElement(ns + "container",
                new XAttribute("version", "2.0"));

            XElement filesElement = new XElement(ns + "rootfiles");
            foreach (RootFile r in _rootFiles)
            {
                XElement fileElement = new XElement(ns + "rootfile",
                    new XAttribute("full-path", r.file),
                    new XAttribute("media-type", r.mediaType));
                filesElement.Add(fileElement);
            }
            element.Add(filesElement);

            return element;
        }
    }
}
