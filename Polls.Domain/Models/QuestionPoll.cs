using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.Models
{
    public class QuestionPoll
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(200)]
        [Required]
        public string Text { get; set; }

        [Required]
        public string Rating { get; set; }

        // Foreign Key
        public int PollId { get; set; }

        // Relational Properties

        [ForeignKey(nameof(PollId))]
        public Poll Poll { get; set; }
    }
}
