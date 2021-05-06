namespace Domain.Dto
{
    public abstract class DishForManipulationDto
    {
        /// <summary>
        ///     The name of the dish
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The description of the dish
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Price of the dish
        /// </summary>
        public decimal Price { get; set; }
    }
}