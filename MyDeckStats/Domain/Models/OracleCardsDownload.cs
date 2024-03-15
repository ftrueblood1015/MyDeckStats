﻿namespace MyDeckStats.Domain.Models
{
    public class OracleCardsDownload
    {
        public string? _object { get; set; }
        public string? id { get; set; }
        public string? type { get; set; }
        public DateTime updated_at { get; set; }
        public string? uri { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public int size { get; set; }
        public string? download_uri { get; set; }
        public string? content_type { get; set; }
        public string? content_encoding { get; set; }
    }
}
