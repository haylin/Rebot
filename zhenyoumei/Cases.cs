using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zhenyoumei
{
    public class Cases
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        public Guid CaseId { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 医生ID
        /// </summary>
        public int DoctorId { get; set; }
        /// <summary>
        /// 派单成交ID
        /// </summary>
        public int CrmSubOrderId { get; set; }
        /// <summary>
        /// 医院ID
        /// </summary>
        public int HospitalId { get; set; }
        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProjectId { get; set; }

      

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 标签名称.
        /// </summary>
        /// <value>
        /// The name of the tag.
        /// </value>
        public string TagName { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// 效果持续.
        /// </summary>
        /// <value>
        /// The result data.
        /// </value>
        public string ResultData { get; set; }

        /// <summary>
        /// 恢复速度.
        /// </summary>
        /// <value>
        /// The result speed.
        /// </value>
        public string ResultSpeed { get; set; }

        /// <summary>
        /// 技术手段
        /// </summary>
        /// <value>
        /// The treatment way.
        /// </value>
        public string TreatmentWay { get; set; }

        /// <summary>
        /// 疗程.
        /// </summary>
        /// <value>
        /// The durations.
        /// </value>
        public string Durations { get; set; }

        /// <summary>
        /// 适用人群
        /// </summary>
        /// <value>
        /// The applicable crowd.
        /// </value>
        public string ApplicableCrowd { get; set; }

        /// <summary>
        /// 是否推荐
        /// </summary>
        public bool IsRecommend { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string SourceDevice { get; set; }
        /// <summary>
        /// 审核状态。
        /// 0：未审核，
        /// 1：审核通过，
        /// 2：审核未通过
        /// </summary>
        public int AuditStatus { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public string AuditAdmin { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime AuditTime { get; set; }
        /// <summary>
        /// 返款状态
        /// </summary>
        public bool RebateStatus { get; set; }
        /// <summary>
        /// 反抗金额
        /// </summary>
        public decimal RebateAmount { get; set; }
        /// <summary>
        /// 返款时间
        /// </summary>
        public DateTime RebateTime { get; set; }
        /// <summary>
        /// 发票图片
        /// </summary>
        public string InvoiceImage { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 医院名称
        /// </summary>
        public string HospitalName { get; set; }
    }
}
