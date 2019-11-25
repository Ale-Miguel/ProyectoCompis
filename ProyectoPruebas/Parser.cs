
using System;



public class Parser {
	public const int _EOF = 0;
	public const int _Id = 1;
	public const int _Program = 2;
	public const int _Var = 3;
	public const int _Int = 4;
	public const int _Float = 5;
	public const int _Print = 6;
	public const int _Comma = 7;
	public const int _Colon = 8;
	public const int _Semicolon = 9;
	public const int _If = 10;
	public const int _Else = 11;
	public const int _ElseIf = 12;
	public const int _EndIf = 13;
	public const int _LeftParenthesis = 14;
	public const int _RightParenthesis = 15;
	public const int _LeftCurlyBracket = 16;
	public const int _RightCurlyBracket = 17;
	public const int _CTE_I = 18;
	public const int _CTE_F = 19;
	public const int _CTE_S = 20;
	public const int _Plus = 21;
	public const int _Minus = 22;
	public const int _Asterisk = 23;
	public const int _Slash = 24;
	public const int _Equal = 25;
	public const int _EqualThan = 26;
	public const int _GreaterThan = 27;
	public const int _LesserThan = 28;
	public const int _NotEqual = 29;
	public const int _GreaterThanOrEqual = 30;
	public const int _LessThanOrEqual = 31;
	public const int _Function = 32;
	public const int _Void = 33;
	public const int _Return = 34;
	public const int _EndFunction = 35;
	public const int _Bool = 36;
	public const int _String = 37;
	public const int _TurnLeft = 38;
	public const int _TurnRight = 39;
	public const int _Shoot = 40;
	public const int _Wait = 41;
	public const int _MoveForward = 42;
	public const int _Interact = 43;
	public const int _Pick = 44;
	public const int _Drop = 45;
	public const int _For = 46;
	public const int _EndFor = 47;
	public const int _Loop = 48;
	public const int _EndLoop = 49;
	public const int maxT = 50;

	const bool _T = true;
	const bool _x = false;
	const int minErrDist = 2;
	
	public Scanner scanner;
	public Errors  errors;

	public Token t;    // last recognized token
	public Token la;   // lookahead token
	int errDist = minErrDist;

string variableName;
  string functionName;
  string signSymbol;

  private ProyectoPruebas.VarTable tab;
 private ProyectoPruebas.ErrorHandler errorHandler;

  int constCont = 0;

  internal ProyectoPruebas.VarTable Tab { get => Tab1; set => Tab1 = value; }
  internal ProyectoPruebas.VarTable Tab1 { get => tab; set => tab = value; }
  internal ProyectoPruebas.ErrorHandler ErrorHandler { get => errorHandler; set => errorHandler = value; }




	public Parser(Scanner scanner) {
		this.scanner = scanner;
		errors = new Errors();
	}

	void SynErr (int n) {
		if (errDist >= minErrDist) errors.SynErr(la.line, la.col, n);
		errDist = 0;
	}

	public void SemErr (string msg) {
		if (errDist >= minErrDist) errors.SemErr(t.line, t.col, msg);
		errDist = 0;
	}
	
	void Get () {
		for (;;) {
			t = la;
			la = scanner.Scan();
			if (la.kind <= maxT) { ++errDist; break; }

			la = t;
		}
	}
	
	void Expect (int n) {
		if (la.kind==n) Get(); else { SynErr(n); }
	}
	
	bool StartOf (int s) {
		return set[s, la.kind];
	}
	
	void ExpectWeak (int n, int follow) {
		if (la.kind == n) Get();
		else {
			SynErr(n);
			while (!StartOf(follow)) Get();
		}
	}


	bool WeakSeparator(int n, int syFol, int repFol) {
		int kind = la.kind;
		if (kind == n) {Get(); return true;}
		else if (StartOf(repFol)) {return false;}
		else {
			SynErr(n);
			while (!(set[syFol, kind] || set[repFol, kind] || set[0, kind])) {
				Get();
				kind = la.kind;
			}
			return StartOf(syFol);
		}
	}

	
	void ProyectoFinal() {
		PROGRAM();
	}

	void PROGRAM() {
		VARS();
		tab.codeGenerator.pushGoToMain();
		while (la.kind == 32) {
			MODULE();
		}
		tab.codeGenerator.solveGoToMain();
		
		tab.createContext();
		STATUTE();
		while (StartOf(1)) {
			STATUTE();
		}
		tab.codeGenerator.endFunction();
	}

	void VARS() {
		while (la.kind == 3) {
			VAR();
		}
	}

	void MODULE() {
		Expect(32);
		string functName; int type;
		if (StartOf(2)) {
			TYPE();
		} else if (la.kind == 33) {
			Get();
		} else SynErr(51);
		type = t.kind;
		Expect(1);
		functName = t.val; 
		ProyectoPruebas.Function fun = new ProyectoPruebas.Function(functName, type);
		tab.addFunction(fun);
		
		PARAMS();
		Expect(8);
		STATUTE();
		while (StartOf(1)) {
			STATUTE();
		}
		Expect(35);
		tab.codeGenerator.endFunction();
		
	}

	void STATUTE() {
		if (StartOf(3)) {
			COMMAND();
		} else if (la.kind == 10) {
			CONDITION();
		} else if (la.kind == 46 || la.kind == 48) {
			CYCLE();
		} else if (la.kind == 1) {
			ASGMT_OR_FUNCT();
		} else if (la.kind == 34) {
			MODULE_RETURN();
		} else SynErr(52);
	}

	void VAR() {
		Expect(3);
		string name; int type; 
		TYPE();
		type = t.kind;
		Expect(1);
		name =  t.val; 
		ProyectoPruebas.Variable var = new ProyectoPruebas.Variable(name, type);
		tab.addVariable(var);
		tab.codeGenerator.pushSymbolStack(var);
		
		Expect(25);
		tab.codeGenerator.pushOperatorStack(t.kind);
		if (la.kind == 21 || la.kind == 22) {
			SIGNS();
		}
		if (la.kind == 18 || la.kind == 19) {
			NUMBER();
		} else if (la.kind == 20) {
			Get();
		} else SynErr(53);
		string constNumber = "-";
		
		if(signSymbol == "-"){
		 constNumber +=  t.val;
		 signSymbol = "+";
		}
		else{
		 constNumber = t.val;
		}
		
		int constType = t.kind;
		
		ProyectoPruebas.Variable constVar = new ProyectoPruebas.Variable(constNumber, constType);
		
		constVar.setConstant();
		
		constVar = tab.addConstant(constVar);
		
		tab.codeGenerator.pushSymbolStack(constVar);
		
		tab.codeGenerator.solveAssignment();
		Expect(9);
	}

	void TYPE() {
		if (la.kind == 5) {
			Get();
		} else if (la.kind == 4) {
			Get();
		} else if (la.kind == 37) {
			Get();
		} else if (la.kind == 36) {
			Get();
		} else SynErr(54);
	}

	void SIGNS() {
		if (la.kind == 21) {
			Get();
		} else if (la.kind == 22) {
			Get();
			signSymbol = "-";
		} else SynErr(55);
	}

	void NUMBER() {
		if (la.kind == 18) {
			Get();
		} else if (la.kind == 19) {
			Get();
		} else SynErr(56);
	}

	void PARAMS() {
		Expect(14);
		tab.createContext();
		if (StartOf(2)) {
			PARAMS_1();
		}
		Expect(15);
	}

	void MODULE_RETURN() {
		Expect(34);
		if (StartOf(4)) {
			EXPR();
		}
		if(!tab.codeGenerator.createReturn(t.kind)){
		 errorHandler.SemErr("Void function can't return a value");
		}
		Expect(9);
	}

	void EXPR() {
		TERM();
		while (la.kind == 21 || la.kind == 22) {
			if (la.kind == 21) {
				Get();
			} else {
				Get();
			}
			tab.codeGenerator.pushOperatorStack(t.kind);
			TERM();
			if(!tab.codeGenerator.solveSumAndMinus()){
			 errorHandler.SemErr("Type-mismatch");
			}
			
			
		}
	}

	void PARAMS_1() {
		TYPE();
		int paramType = t.kind;
		Expect(1);
		ProyectoPruebas.Variable param = new ProyectoPruebas.Variable(t.val, paramType);
		tab.addParamToFunction(param);
		
		while (la.kind == 7) {
			PARAMS_2();
		}
	}

	void PARAMS_2() {
		Expect(7);
		TYPE();
		int paramType = t.kind;
		Expect(1);
		ProyectoPruebas.Variable param = new ProyectoPruebas.Variable(t.val, paramType);
		
		tab.addParamToFunction(param);
		
	}

	void COMMAND() {
		COMMAND_LIST();
		tab.codeGenerator.createCommand(t.kind);
		Expect(9);
	}

	void COMMAND_LIST() {
		switch (la.kind) {
		case 38: {
			Get();
			break;
		}
		case 39: {
			Get();
			break;
		}
		case 40: {
			Get();
			break;
		}
		case 41: {
			Get();
			break;
		}
		case 42: {
			Get();
			break;
		}
		case 43: {
			Get();
			break;
		}
		case 44: {
			Get();
			break;
		}
		case 45: {
			Get();
			break;
		}
		case 6: {
			PRINT();
			break;
		}
		default: SynErr(57); break;
		}
	}

	void PRINT() {
		Expect(6);
		Expect(14);
		if (la.kind == 20) {
			Get();
		} else if (la.kind == 1) {
			Get();
		} else SynErr(58);
		ProyectoPruebas.Variable printVar;
		if(t.kind == _CTE_S){
		   printVar = new ProyectoPruebas.Variable(t.val, t.kind, t.val);
		   printVar = tab.addConstant(printVar);
		   
		}
		else{
		  printVar = tab.findVariable(t.val);
		}
		
		if(printVar == null){
		   errorHandler.SemErr("Variable " + t.val + " not declared");
		}
		else{
		 tab.codeGenerator.solvePrint(printVar);
		}
		
		Expect(15);
	}

	void CYCLE() {
		if (la.kind == 46) {
			FOR();
		} else if (la.kind == 48) {
			LOOP();
		} else SynErr(59);
	}

	void FOR() {
		Expect(46);
		tab.addVariableLayer();
		tab.codeGenerator.pushJumpStack(tab.codeGenerator.getLineCont() ); 
		Expect(14);
		EXPRESSION();
		Expect(15);
		ProyectoPruebas.Variable result = tab.codeGenerator.popSymnbolStack();
		
		if(result.getType() != ProyectoPruebas.OperationTypes.TYPE_BOOL){
		
		   errorHandler.SemErr("Type-mismatch: Expected a bool type operation");
		}
		else{
		
		 tab.codeGenerator.pushGoToF(result);
		}
		
		STATUTE();
		while (StartOf(1)) {
			STATUTE();
		}
		Expect(47);
		tab.codeGenerator.solveFor();
		tab.removeVariableLayer(); 
	}

	void LOOP() {
		Expect(48);
		tab.addVariableLayer();
		if (la.kind == 18) {
			Get();
		} else if (la.kind == 1) {
			Get();
		} else SynErr(60);
		tab.codeGenerator.createLoopCondition(new ProyectoPruebas.Variable(t.val, t.kind));
		STATUTE();
		while (StartOf(1)) {
			STATUTE();
		}
		Expect(49);
		tab.codeGenerator.solveLoop();
		tab.removeVariableLayer();
	}

	void EXPRESSION() {
		EXPR();
		if (StartOf(5)) {
			EXPRESSION_1();
		}
	}

	void CONDITION() {
		Expect(10);
		Expect(14);
		EXPRESSION();
		Expect(15);
		ProyectoPruebas.Variable result = tab.codeGenerator.popSymnbolStack();
		if(result.getType() != ProyectoPruebas.OperationTypes.TYPE_BOOL){
		
		    errorHandler.SemErr("Type-mismatch: Expected a bool type operation");
		}
		else{
		
		  tab.codeGenerator.pushGoToF(result);
		  tab.addVariableLayer();
		}
		
		STATUTE();
		while (StartOf(1)) {
			STATUTE();
		}
		if (la.kind == 11 || la.kind == 12) {
			CONDITION_ELSE();
		}
		Expect(13);
		tab.codeGenerator.popJumpStack();
		tab.removeVariableLayer();
	}

	void CONDITION_ELSE() {
		if (la.kind == 11) {
			ELSE();
		} else if (la.kind == 12) {
			ELSEIF();
		} else SynErr(61);
	}

	void ELSE() {
		Expect(11);
		tab.codeGenerator.popJumpStack();
		       tab.codeGenerator.pushGoTo();
		    
		STATUTE();
		while (StartOf(1)) {
			STATUTE();
		}
	}

	void ELSEIF() {
		Expect(12);
		tab.codeGenerator.popJumpStack();
		tab.codeGenerator.pushGoTo();
		Expect(14);
		EXPRESSION();
		Expect(15);
		ProyectoPruebas.Variable result = tab.codeGenerator.popSymnbolStack();
		if(result.getType() != ProyectoPruebas.OperationTypes.TYPE_BOOL){
		
		    errorHandler.SemErr("Type-mismatch: Expected a bool type operation");
		}
		else{
		
		  tab.codeGenerator.pushGoToF(result);
		
		}
		
		STATUTE();
		while (StartOf(1)) {
			STATUTE();
		}
		tab.codeGenerator.popJumpStack();
		
		if (la.kind == 11 || la.kind == 12) {
			CONDITION_ELSE();
		}
	}

	void ASGMT_OR_FUNCT() {
		Expect(1);
		variableName = t.val; 
		if (la.kind == 14) {
			FUNCTCALL();
		} else if (la.kind == 25) {
			ASSIGNMENT();
		} else SynErr(62);
		Expect(9);
	}

	void FUNCTCALL() {
		Expect(14);
		ProyectoPruebas.Function fun = tab.findFunction(variableName);
		if(fun == null){
		    errorHandler.SemErr("Function " + variableName + " not declared");
		}
		else{
		
		    tab.codeGenerator.functionCall(fun);
		
		
		}
		if (StartOf(4)) {
			FUNCT_PARAMS();
		}
		Expect(15);
		if(!tab.codeGenerator.solveFunction()){
		
		 errorHandler.SemErr("Invalid number of arguments");
		}
		
		
	}

	void ASSIGNMENT() {
		Expect(25);
		ProyectoPruebas.Variable var = tab.findVariable(variableName);
		if(var == null){
		errorHandler.SemErr("Variable " + variableName +  " not declared");
		}
		else{
		tab.codeGenerator.pushOperatorStack(t.kind);
		tab.codeGenerator.pushSymbolStack(var);
		}
		
		if (StartOf(4)) {
			EXPR();
		} else if (la.kind == 20) {
			Get();
		} else SynErr(63);
		if(!tab.codeGenerator.solveAssignment()){
		     errorHandler.SemErr("Type-mismatch at assignment");
		}
	}

	void FUNCT_PARAMS() {
		EXPR();
		if(!tab.codeGenerator.setFunctParam()){
		   errorHandler.SemErr("Parameters type mismatch");
		}
		
		while (la.kind == 7) {
			Get();
			EXPR();
			if(!tab.codeGenerator.setFunctParam()){
			errorHandler.SemErr("Parameters type mismatch");
			}
			
		}
	}

	void EXPRESSION_1() {
		RELOPS();
		tab.codeGenerator.pushOperatorStack(t.kind);
		EXPR();
		tab.codeGenerator.solveRelOp();
	}

	void RELOPS() {
		switch (la.kind) {
		case 26: {
			Get();
			break;
		}
		case 27: {
			Get();
			break;
		}
		case 30: {
			Get();
			break;
		}
		case 28: {
			Get();
			break;
		}
		case 31: {
			Get();
			break;
		}
		case 29: {
			Get();
			break;
		}
		default: SynErr(64); break;
		}
	}

	void TERM() {
		FACTOR();
		while (la.kind == 23 || la.kind == 24) {
			if (la.kind == 23) {
				Get();
			} else {
				Get();
			}
			tab.codeGenerator.pushOperatorStack(t.kind);
			FACTOR();
			if(!tab.codeGenerator.solveMultAndDiv()){
			   errorHandler.SemErr("Type-mismatch");
			}
			
		}
	}

	void FACTOR() {
		if (la.kind == 14) {
			FACTOR_1();
		} else if (StartOf(6)) {
			FACTOR_2();
		} else SynErr(65);
	}

	void FACTOR_1() {
		Expect(14);
		tab.codeGenerator.pushOperatorStack(t.kind);
		EXPR();
		Expect(15);
		tab.codeGenerator.popOperatorStack();
	}

	void FACTOR_2() {
		if (la.kind == 21 || la.kind == 22) {
			SIGNS();
		}
		FACTOR_VALUES();
	}

	void FACTOR_VALUES() {
		if (la.kind == 1) {
			Get();
			variableName = t.val;
			
			//Variable que dice si entrÃ³ a una funciÃ³n o no
			bool enterFunction = false;
			if (la.kind == 14) {
				enterFunction = true; 
				FUNCTCALL();
			}
			if(!enterFunction){
			 ProyectoPruebas.Variable varId = tab.findVariable(variableName);
			 if(varId != null){
			    tab.codeGenerator.pushSymbolStack(varId);
			 }
			 else{
			   errorHandler.SemErr("Variable " + variableName + " not declared.");
			 }
			}
		} else if (la.kind == 18 || la.kind == 19) {
			NUMBER();
			string constNumber = "-";
			if(signSymbol == "-"){
			   constNumber +=  t.val;
			   signSymbol = "+";
			}
			else{
			 constNumber = t.val;
			}
			
			int constType = t.kind;
			
			ProyectoPruebas.Variable constVar = new ProyectoPruebas.Variable(constNumber, constType);
			
			constVar.setConstant();
			
			constVar = tab.addConstant(constVar);
			
			tab.codeGenerator.pushSymbolStack(constVar);
			
		} else SynErr(66);
	}



	public void Parse() {
		la = new Token();
		la.val = "";		
		Get();
		ProyectoFinal();
		Expect(0);

	}
	
	static readonly bool[,] set = {
		{_T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x},
		{_x,_T,_x,_x, _x,_x,_T,_x, _x,_x,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_T,_x, _x,_x,_T,_T, _T,_T,_T,_T, _T,_T,_T,_x, _T,_x,_x,_x},
		{_x,_x,_x,_x, _T,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _T,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x},
		{_x,_x,_x,_x, _x,_x,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_T,_T, _T,_T,_T,_T, _T,_T,_x,_x, _x,_x,_x,_x},
		{_x,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_T,_x, _x,_x,_T,_T, _x,_T,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_T,_T, _T,_T,_T,_T, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x},
		{_x,_T,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_T,_T, _x,_T,_T,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x}

	};
} // end Parser


public class Errors {
	public int count = 0;                                    // number of errors detected
	public System.IO.TextWriter errorStream = Console.Out;   // error messages go to this stream
	public string errMsgFormat = "-- line {0} col {1}: {2}"; // 0=line, 1=column, 2=text

	public virtual void SynErr (int line, int col, int n) {
		string s;
		switch (n) {
			case 0: s = "EOF expected"; break;
			case 1: s = "Id expected"; break;
			case 2: s = "Program expected"; break;
			case 3: s = "Var expected"; break;
			case 4: s = "Int expected"; break;
			case 5: s = "Float expected"; break;
			case 6: s = "Print expected"; break;
			case 7: s = "Comma expected"; break;
			case 8: s = "Colon expected"; break;
			case 9: s = "Semicolon expected"; break;
			case 10: s = "If expected"; break;
			case 11: s = "Else expected"; break;
			case 12: s = "ElseIf expected"; break;
			case 13: s = "EndIf expected"; break;
			case 14: s = "LeftParenthesis expected"; break;
			case 15: s = "RightParenthesis expected"; break;
			case 16: s = "LeftCurlyBracket expected"; break;
			case 17: s = "RightCurlyBracket expected"; break;
			case 18: s = "CTE_I expected"; break;
			case 19: s = "CTE_F expected"; break;
			case 20: s = "CTE_S expected"; break;
			case 21: s = "Plus expected"; break;
			case 22: s = "Minus expected"; break;
			case 23: s = "Asterisk expected"; break;
			case 24: s = "Slash expected"; break;
			case 25: s = "Equal expected"; break;
			case 26: s = "EqualThan expected"; break;
			case 27: s = "GreaterThan expected"; break;
			case 28: s = "LesserThan expected"; break;
			case 29: s = "NotEqual expected"; break;
			case 30: s = "GreaterThanOrEqual expected"; break;
			case 31: s = "LessThanOrEqual expected"; break;
			case 32: s = "Function expected"; break;
			case 33: s = "Void expected"; break;
			case 34: s = "Return expected"; break;
			case 35: s = "EndFunction expected"; break;
			case 36: s = "Bool expected"; break;
			case 37: s = "String expected"; break;
			case 38: s = "TurnLeft expected"; break;
			case 39: s = "TurnRight expected"; break;
			case 40: s = "Shoot expected"; break;
			case 41: s = "Wait expected"; break;
			case 42: s = "MoveForward expected"; break;
			case 43: s = "Interact expected"; break;
			case 44: s = "Pick expected"; break;
			case 45: s = "Drop expected"; break;
			case 46: s = "For expected"; break;
			case 47: s = "EndFor expected"; break;
			case 48: s = "Loop expected"; break;
			case 49: s = "EndLoop expected"; break;
			case 50: s = "??? expected"; break;
			case 51: s = "invalid MODULE"; break;
			case 52: s = "invalid STATUTE"; break;
			case 53: s = "invalid VAR"; break;
			case 54: s = "invalid TYPE"; break;
			case 55: s = "invalid SIGNS"; break;
			case 56: s = "invalid NUMBER"; break;
			case 57: s = "invalid COMMAND_LIST"; break;
			case 58: s = "invalid PRINT"; break;
			case 59: s = "invalid CYCLE"; break;
			case 60: s = "invalid LOOP"; break;
			case 61: s = "invalid CONDITION_ELSE"; break;
			case 62: s = "invalid ASGMT_OR_FUNCT"; break;
			case 63: s = "invalid ASSIGNMENT"; break;
			case 64: s = "invalid RELOPS"; break;
			case 65: s = "invalid FACTOR"; break;
			case 66: s = "invalid FACTOR_VALUES"; break;

			default: s = "error " + n; break;
		}
		errorStream.WriteLine(errMsgFormat, line, col, s);
		count++;
	}

	public virtual void SemErr (int line, int col, string s) {
		errorStream.WriteLine(errMsgFormat, line, col, s);
		count++;
	}
	
	public virtual void SemErr (string s) {
		errorStream.WriteLine(s);
		count++;
	}
	
	public virtual void Warning (int line, int col, string s) {
		errorStream.WriteLine(errMsgFormat, line, col, s);
	}
	
	public virtual void Warning(string s) {
		errorStream.WriteLine(s);
	}
} // Errors


public class FatalError: Exception {
	public FatalError(string m): base(m) {}
}
