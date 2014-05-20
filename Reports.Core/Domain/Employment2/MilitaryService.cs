using System;

namespace Reports.Core.Domain
{
    public class MilitaryService : AbstractEntityWithVersion
    {
        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Properties
        public virtual bool? IsLiableForMilitaryService { get; set; } // OK
        public virtual string MilitaryCardNumber { get; set; } // OK
        public virtual DateTime? MilitaryCardDate { get; set; } // OK
        public virtual MilitaryReserveCategory ReserveCategory { get; set; } // OK
        public virtual MilitaryRank Rank { get; set; } // OK
        public virtual MilitarySpecialityCategory SpecialityCategory { get; set; } // ������ (�������) | SpecialityCategory
        public virtual string MilitarySpecialityCode { get; set; } // OK
        public virtual string CombatFitness { get; set; } // ?
        public virtual string Commissariat { get; set; } // OK
        // ������� �� �������� �����? | MilitaryServiceRegistration
        // ��������� ��������� | MilitaryPersonnelCategory
        // ��� | MilitaryPersonnelType
        // ��������������� ����������� | ?
        // ������ �� ������� ������ | ?
        // ���� �������� ������
        // ���� ���������������� ������
        #endregion

        #region Constructors
        #endregion

        #region Methods
        #endregion

        #region System.Object overrides
        #endregion

        #region MetaData
        #endregion
    }
}