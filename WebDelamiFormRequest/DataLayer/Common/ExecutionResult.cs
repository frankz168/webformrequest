public class ExecutionResult
{

    public string MessageText = "";

    public string MessageException = "";

    public RESULTTYPE Result = RESULTTYPE.NOPROCESS;

    public bool IsHasResult = false;
}

public enum RESULTTYPE
{

    SUCCESS,

    FAILED,

    NOPROCESS,

    EMPTY,
}