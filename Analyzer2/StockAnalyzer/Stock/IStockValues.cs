using System;
using System.Collections.Generic;
using System.Text;
using Stock.Common.Data;

namespace FinanceAnalyzer.Stock
{
    // �������н�����ÿһ�����ֵ����Ʊ�ʲ����ֽ� 
    public interface IStockValues
    {
        // �õ�ĳ�����ֵ
        double GetTotalValue(DateTime dt);

        // ����ĳ�����ֵ
        void SetTotalValue(DateTime dt, double val);

        // �õ����е�����
        ICollection<DateTime> GetAllDate();

        // ���õ��ղ�������
        void SetOperationSignal(DateTime dt, OperType val);

        // ��ȡ���ղ�������
        OperType GetOperationSignal(DateTime dt);

        // Get the total count of specified opertype 
        int GetOperCount(OperType oper);
    }
}
