using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Globalization;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Context;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories
{
    public class TurnosSemanaPdf : IDocument
    {
        private readonly List<DiaSemanaDto> _dias;
        private readonly int _semana;
        private readonly int _anio;
        private readonly string _semestre;
        private readonly int _ultimoturno;
        private readonly IWebHostEnvironment _env;


        public TurnosSemanaPdf(
       IWebHostEnvironment? env,
       List<DiaSemanaDto> dias,
       int semana,
       int anio,
       string semestre,
       int ultimoturno)
        {
            _env = env;
            _dias = dias;
            _semana = semana;
            _anio = anio;
            _semestre = semestre;
            _ultimoturno = ultimoturno;
        }



        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4.Landscape());

                page.MarginTop(55);      // 22 mm
                page.MarginBottom(55);   // 22 mm
                page.MarginHorizontal(60);


                page.Header()
                    .Element(BuildHeader);

                page.Content()
                    .Element(BuildTable);

                page.Footer()
                    .PaddingTop(21.6f)
                    .Element(BuildFooter);
            });
        }


        // ========================= HEADER =========================
        private void BuildHeader(IContainer container)
        {
            container.Column(col =>
            {
                var logoPath = Path.Combine(
                    _env.ContentRootPath,
                    "Img",
                    "logohd.png"
                );


                col.Item().Border(1).Row(row =>
                {
                    row.ConstantItem(110)
                       .AlignCenter()
                       .Image(logoPath);

                    row.RelativeItem().AlignCenter().Column(c =>
                    {
                        c.Spacing(4);

                        c.Item().AlignCenter().Text("UNIVERSIDAD COLEGIO MAYOR DE CUNDINAMARCA")
                            .Bold().FontSize(8)
                            .FontColor(Colors.Blue.Darken2);

                        c.Item().AlignCenter().Text("FACULTAD DE DERECHO")
                            .Bold().FontSize(8);

                        c.Item().AlignCenter().Text("CONSULTORIO JURÍDICO Y CENTRO DE CONCILIACIÓN")
                            .FontSize(8);

                        c.Item().AlignCenter().Text(
                            "SEDE. CONVENIO UPK - TINTAL \"UNIVERSIDAD PÚBLICA EN KENNEDY\"")
                            .FontSize(8)
                            .FontColor(Colors.Grey.Darken1);

                        c.Item().PaddingVertical(2).LineHorizontal(1);

                        c.Item().AlignCenter().Text(
                            $"LISTADO DE ASIGNACIÓN DE TURNOS PARA ESTUDIANTES PERIODO {_anio} - {_semestre}")
                            .Bold().FontSize(8);

                        c.Item().AlignCenter().Text($"SEMANA NO. {_semana}")
                            .Bold().FontSize(8);
                    });
                });
            });
        }


        // ========================= TABLE =========================
        private void BuildTable(IContainer container)
        {
            container.Column(col =>
            {
                col.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(28);
                        columns.RelativeColumn(5);
                        columns.ConstantColumn(20);
                        columns.RelativeColumn(1);
                        columns.ConstantColumn(70);
                        columns.ConstantColumn(55);
                        columns.RelativeColumn(3);
                        columns.RelativeColumn(3);
                        columns.RelativeColumn(3);
                    });

                    BuildTableHeader(table);
                    BuildTableBody(table);
                });
            });
        }


        private void BuildTableHeader(TableDescriptor table)
        {
            void Header(string text) =>
                table.Cell()
                     .Element(CellStyle)
                     .AlignCenter()
                     .Text(text)
                     .Bold()
                     .FontSize(8);

            Header("No");
            Header("Estudiante");
            Header("CJ");
            Header("Día");
            Header("Fecha");
            Header("Jornada");
            Header("Firma Est.");
            Header("Asesor");
            Header("Firma Asesor");
        }

        private void BuildTableBody(TableDescriptor table)
        {
            
            int contador =  _ultimoturno;

            foreach (var dia in _dias)
            {
                var filas = BuildFilasDia(dia);

                if (!filas.Any())
                {
                    table.Cell()
                        .ColumnSpan(9)
                        .Element(CenterStyle)
                        .AlignCenter()
                        .Text("Sin actividad asignada")
                        .Italic();
                    continue;
                }

                bool primeraFila = true;

                foreach (var fila in filas)
                {
                    table.Cell().Element(CenterStyle).AlignCenter()
                        .Text(fila.Estudiante != null ? contador++.ToString() : "—")
                        .FontSize(7);

                    table.Cell().Element(CenterStyle)
                        .Text(fila.Estudiante?.Nombre ?? "")
                        .FontSize(7);

                    table.Cell().Element(CenterStyle).AlignCenter()
                        .Text(fila.Estudiante?.ConsultorioId.ToString() ?? "")
                        .FontSize(7);

                    if (primeraFila)
                    {
                        table.Cell()
                        .RowSpan((uint)filas.Count)
                        .Element(container =>
                        {
                            container
                                .AlignCenter()   // horizontal
                                .AlignMiddle()   // vertical
                                .Text(dia.Dia)
                                .FontSize(9)
                                .Bold();
                        });

                        primeraFila = false;
                    }

                        table.Cell().Element(CenterStyle).AlignCenter()
                            .Text(dia.Fecha
                            .ToString("dd/MMM/yyyy", new CultureInfo("es-ES"))
                            .ToUpper())
                            .FontSize(7);

                    table.Cell().Element(CenterStyle).AlignCenter()
                        .Text(fila.Jornada)
                        .FontSize(7);

                    table.Cell().Element(CenterStyle).Text("")
                        .FontSize(7);

                    table.Cell().Element(CenterStyle)
                        .Text(fila.Asesor?.Nombre ?? "SIN ASESOR")
                        .FontSize(7);

                    table.Cell().Element(CenterStyle).Text("")
                        .FontSize(7);
                }

                table.Cell()
                    .ColumnSpan(9)
                    .Height(1)
                    .Background(Colors.Black);
            }
        }

        // ========================= CELL STYLE =========================
        private IContainer CellStyle(IContainer container)
        {
            return container
                .Border(1)
                .Padding(4);
        }

        private IContainer CenterStyle(IContainer container)
        {
            return container
                .Border(1)
                .Padding(2)
                .AlignCenter()   // horizontal
                .AlignMiddle();   // vertical
        }

        // ========================= FILAS =========================
        private List<FilaPdf> BuildFilasDia(DiaSemanaDto dia)
        {
            var filas = new List<FilaPdf>();
            var jornadas = new[] { "AM", "PM" };

            foreach (var jornada in jornadas)
            {
                var estudiantes = dia.Turnos.Where(t => t.Jornada == jornada).ToList();
                var asesores = dia.Asesor.Where(a => a.Jornada == jornada).ToList();

                int max = Math.Max(estudiantes.Count, asesores.Count);

                for (int i = 0; i < max; i++)
                {
                    filas.Add(new FilaPdf
                    {
                        Jornada = jornada,
                        Estudiante = estudiantes.ElementAtOrDefault(i),
                        Asesor = asesores.ElementAtOrDefault(i)
                    });
                }
            }

            return filas;
        }

        // ========================= FOOTER =========================
        private void BuildFooter(IContainer container)
        {
            container.Column(col =>
            {
                col.Item().PaddingTop(10).LineHorizontal(1);

                col.Item().AlignRight()
                    .Text("Generado automáticamente por el sistema")
                    .FontSize(9)
                    .Italic();
            });
        }

    }
}
