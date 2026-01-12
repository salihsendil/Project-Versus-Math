public class OperationsSelectionChangedSignal {
    public OperationTypes Operation;
    public bool IsAllowed;

    public OperationsSelectionChangedSignal(OperationTypes operation, bool isAllowed)
    {
        Operation = operation;
        IsAllowed = isAllowed;
    }
}