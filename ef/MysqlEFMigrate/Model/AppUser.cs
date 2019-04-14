using System;
using System.Collections.Generic;

namespace User.API.Model
{
    public class AppUser
    {
        public int Id { get; set; }

        /// <summary>
        /// ͷ���ַ
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// ��˾
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// ְλ
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProvinceId { get; set; }


        /// <summary>
        /// �Ա� 1���� 0��Ů
        /// </summary>
        public byte Gender { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CityId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string NameCard { get; set; }

        public List<UserProperty> properties { get; set; }



    }
}