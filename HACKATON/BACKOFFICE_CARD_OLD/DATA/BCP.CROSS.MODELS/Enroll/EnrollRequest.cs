using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BCP.CROSS.MODELS.Enroll
{
    public class EnrollRequest
    {
        public string publicToken { get; set; }
        public string appUserId { get; set; }
        public int categoryId { get; set; }
        public List<Commerce> commerce { get; set; }
        public List<ServiceEnroll> serviceEnroll { get; set; }
        public string domainUser { get; set; }
        public class Commerce
        {
            public int id { get; set; }
            [Required(ErrorMessage = "Campo requerido")]
            public string name { get; set; }
            [Required(ErrorMessage = "Campo requerido")]
            public string commerceCode { get; set; }
            [Required(ErrorMessage = "Campo requerido")]
            public string businessName { get; set; }
            [Required(ErrorMessage = "Campo requerido")]
            public string nit { get; set; }
            [Required(ErrorMessage = "Campo requerido")]
            public string phoneNumber { get; set; }
            [Required(ErrorMessage = "Campo requerido")]
            public string city { get; set; }
            public string logoUrl { get; set; }
            public string socialNetworkLink { get; set; }
        }
        public class ServiceEnroll
        {
            [Required(ErrorMessage = "Campo requerido")]
            public string serviceCode { get; set; }
            [Required(ErrorMessage = "Campo requerido")]
            public string service { get; set; }
            [Required(ErrorMessage = "Campo requerido")]
            public string description { get; set; }
            [Required(ErrorMessage = "Campo requerido")]
            public string startDate { get; set; }
            [Required(ErrorMessage = "Campo requerido")]
            public string endDate { get; set; }
            [Required(ErrorMessage = "Campo requerido")]
            public string discountText { get; set; }
            [Required(ErrorMessage = "Campo requerido")]
            public int discountAmount { get; set; }
            [Required(ErrorMessage = "Campo requerido")]
            public int maximumAmount { get; set; }
            [Required(ErrorMessage = "Campo requerido")]
            public int discountRate { get; set; }
            [Required(ErrorMessage = "Campo requerido")]
            public string disclaimer { get; set; }
            [Required(ErrorMessage = "Campo requerido")]
            public string stock { get; set; }
            [Required(ErrorMessage = "Campo requerido")]
            public string type { get; set; }
            [Required(ErrorMessage = "Campo requerido")]
            public string serviceCity { get; set; }
            [Required(ErrorMessage = "Campo requerido")]
            public string largeImageUrl { get; set; }
            [Required(ErrorMessage = "Campo requerido")]
            public string smallImageUrl { get; set; }
            public string template { get; set; }
            [Required(ErrorMessage = "Campo requerido")]
            public string codeType { get; set; }
            [Required(ErrorMessage = "Campo requerido")]
            public string formatCode { get; set; }
            [Required(ErrorMessage = "Campo requerido")]
            public int axisX { get; set; }
            [Required(ErrorMessage = "Campo requerido")]
            public int axisY { get; set; }
            [Required(ErrorMessage = "Campo requerido")]
            public int codeWidth { get; set; }
            [Required(ErrorMessage = "Campo requerido")]
            public int codeHigh { get; set; }
            [Required(ErrorMessage = "Campo requerido")]
            public int marginCode { get; set; }
            [Required(ErrorMessage = "Campo requerido")]
            public string encryption { get; set; }
            [Required(ErrorMessage = "Campo requerido")]
            public string serviceState { get; set; }
        }
    }
    public class EnrollDTO
    {
        public string publicToken { get; set; }
        public string appUserId { get; set; }
        public int categoryId { get; set; }
        public string domainUser { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string name { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string commerceCode { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string businessName { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string nit { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string phoneNumber { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string city { get; set; }
        public string logoUrl { get; set; }
        public string socialNetworkLink { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string serviceCode { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string service { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string description { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string startDate { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string endDate { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string discountText { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public int discountAmount { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public int maximumAmount { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public int discountRate { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string disclaimer { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string stock { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string type { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string serviceCity { get; set; }
        public string largeImageUrl { get; set; }
        public string smallImageUrl { get; set; }
        public string template { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string codeType { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string formatCode { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public int axisX { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public int axisY { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public int codeWidth { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public int codeHigh { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public int marginCode { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string encryption { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string serviceState { get; set; }
    }
}
