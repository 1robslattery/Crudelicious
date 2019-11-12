    using System.ComponentModel.DataAnnotations;
    using System;
	// using Crudelicious.Models.Validators;

    namespace Crudelicious.Models
    {
        public class dish
        {
            // auto-implemented properties need to match the columns in your table
            // the [Key] attribute is used to mark the Model property being used for your table's Primary Key
            [Key]
            public int DishID {get; set;}
            // MySQL VARCHAR and TEXT types can be represeted by a string

			[Required(ErrorMessage="Name of dish is required")]
			[MinLength(4, ErrorMessage="No abbreviated dish names, please")]
            public string Name {get; set;}

			[Required(ErrorMessage="We must know who the chef is!")]
			[MinLength(5, ErrorMessage="Enter a valid name")]
            public string Chef {get; set;}

			[Required(ErrorMessage="We must know how tasty this dish is!!  Pick a number between 1-5")]
			[Range(1,5)]
            public int Tastiness {get; set;}

			[Required(ErrorMessage="How many calories will this dish be?")]
			[Range(0,4000)]
            public int Calories {get; set;}

			[Required(ErrorMessage="Please describe this dish - be articulate!")]
			[MinLength(25, ErrorMessage="You must enter more than 25 characters!")]
			public string Description {get; set;}

            // The MySQL DATETIME type can be represented by a DateTime
            public DateTime CreatedAt {get;set;} = DateTime.Now;
            public DateTime UpdatedAt {get;set;} = DateTime.Now;
        }
    }