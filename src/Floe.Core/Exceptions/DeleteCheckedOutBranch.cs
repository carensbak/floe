namespace Floe.Core.Exceptions;

public class DeleteCheckedOutBranchException(string branch) : Exception($"Cannot delete {branch}, as it is currently checked out.");
