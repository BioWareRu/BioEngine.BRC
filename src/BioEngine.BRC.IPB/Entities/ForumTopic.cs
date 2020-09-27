﻿using System;

 namespace BioEngine.BRC.IPB.Entities
{
    public class ForumTopic
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public Forum? Forum { get; set; }
        public int Posts { get; set; }
        public int Views { get; set; }
        public string Prefix { get; set; } = "";
        public ForumPost? FirstPost { get; set; }
        public ForumPost? LastPost { get; set; }
        public bool Locked { get; set; }
        public bool Hidden { get; set; }
        public bool Pinned { get; set; }
        public bool Featured { get; set; }
        public bool Archived { get; set; }
        public string Url { get; set; } = "";
    }

    public class TopicCreateModel
    {
        public int? Forum { get; set; }
        public string Title { get; set; } = "";
        public int? Hidden { get; set; }
        public int? Pinned { get; set; }
        public string Post { get; set; } = "";
        public int Author { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
