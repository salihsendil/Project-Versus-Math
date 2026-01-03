public class OperationsSelectionChangedSignal {

    public OperationTypes operation;
    public bool isAllowed;

    public OperationsSelectionChangedSignal(OperationTypes operation, bool isAllowed)
    {
        this.operation = operation;
        this.isAllowed = isAllowed;
    }
}