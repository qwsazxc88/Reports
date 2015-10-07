﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain
{
    public class StaffMovements: StandartRequestEntity
    {
        public virtual StaffMovementsData Data { get; set; }
        //Основные данные заявки                
        	
	    //Подразделения
        /// <summary>
        /// Исходное подразделение
        /// </summary>
        public virtual Department SourceDepartment { get; set; }
        /// <summary>
        /// Целевое подразделение
        /// </summary>
        public virtual Department TargetDepartment { get; set; }
        
	    //Должности
        /// <summary>
        /// Исходная должность
        /// </summary>
        public virtual Position SourcePosition { get; set; }

        /// <summary>
        /// Целевая должность
        /// </summary>
        public virtual Position TargetPosition { get; set; }

	    //Люди                
        /// <summary>
        /// Руководитель исходного подразделения
        /// </summary>
        public virtual User SourceManager { get; set; }
        /// <summary>
        /// Вышестоящий руководитель исходного подразделения
        /// </summary>
        public virtual User SourceChief { get; set; }
        /// <summary>
        /// Руководитель целевого подразделения
        /// </summary>
        public virtual User TargetManager { get; set; }
        /// <summary>
        /// Вышестоящий руководитель целевого подразделения
        /// </summary>
        public virtual User TargetChief { get; set; }
        /// <summary>
        /// Кадровик банка
        /// </summary>
        public virtual User PersonnelManagerBank { get; set; }
        /// <summary>
        /// Кадровик
        /// </summary>
        public virtual User PersonnelManager { get; set; }

	    //Даты
        /// <summary>
        /// Дата приказа о переводе
        /// </summary>
        public virtual DateTime? OrderDate { get; set; }
        
        /// <summary>
        /// Дата перевода
        /// </summary>
        public virtual DateTime? MovementDate { get; set; }
        /// <summary>
        /// Дата временного перевода
        /// </summary>
        public virtual DateTime? MovementTempTo { get; set; }
        /// <summary>
        /// Дата согласования руководителем исходного подразделения
        /// </summary>
        public virtual DateTime? SourceManagerAccept { get; set; }
        /// <summary>
        /// Дата согласования руководителем целевого подразделения
        /// </summary>
        public virtual DateTime? TargetManagerAccept { get; set; }
        /// <summary>
        /// Дата согласования вышестоящим руководителем исходного подразделения
        /// </summary>
        public virtual DateTime? SourceChiefAccept { get; set; }
        /// <summary>
        /// Дата согласования вышестоящим руководителем целевого подразделения
        /// </summary>
        public virtual DateTime? TargetChiefAccept { get; set; }
        /// <summary>
        /// Дата согласования кадровиком банка
        /// </summary>
        public virtual DateTime? PersonnelManagerBankAccept { get; set; }
        /// <summary>
        /// Дата согласования кадровиком
        /// </summary>
        public virtual DateTime? PersonnelManagerAccept { get; set; }

        public virtual IList<StaffMovementsDocs> Docs { get; set; }

        public virtual bool IsTempMoving { get; set; }

    }
}
