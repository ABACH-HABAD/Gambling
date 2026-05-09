namespace DataBaseClasses.Exceptions;

//base
public class DataBaseException : Exception;
public class CannotFindConnectionString : DataBaseException;
public class NoConnectionException : DataBaseException;

//account
public class IncorrectAccountDataException : DataBaseException;
public class AccountNotFoundException : DataBaseException;

//balance
public class BalanceCannotBeNegativeException : DataBaseException;
public class InsufficientFundsException : DataBaseException;
public class NotPossibleToDepositOrWithdrawNegativeAmountOfFundsException : DataBaseException;
