﻿// Copyright 2017 Russlan Akiev
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace RazorPDF
{
    using System.IO;
    using iTextSharp.text;
    using iTextSharp.text.html.simpleparser;
    using iTextSharp.text.pdf;
    using RazorEngine;

    public class PdfParser
    {
        public MemoryStream ParseHtml(string html)
        {
            MemoryStream ms = new MemoryStream();
            Document document = new Document();
            PdfWriter pdfWriter = PdfWriter.GetInstance(document, ms);
            HTMLWorker worker = new HTMLWorker(document);
          
            document.Open();
            worker.StartDocument();
            pdfWriter.CloseStream = false;
            worker.Parse(new StringReader(html));
            worker.EndDocument();
            worker.Close();
            document.CloseDocument();
            document.Close();            
            pdfWriter.Flush();
            pdfWriter.Close();

            return ms;
        }

        public MemoryStream ParseRazor(string razorTemplate)
        {   
            return this.ParseHtml(Razor.Parse(razorTemplate));
        }

        public MemoryStream ParseRazor(string razorTemplate, object model)
        {
            return this.ParseHtml(Razor.Parse(razorTemplate, model));
        }
    }
}
