
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Q8Cs
{
	///TFtdcExchangePropertyType是一个交易所属性类型
	public enum ExchangeProperty 
	{
		Normal = '0',
		GenOrderByTrade = '1',
	}

	///TFtdcIdCardTypeType是一个证件类型类型
	public enum IdCardType 
	{
		EID = '0',
		IDCard = '1',
		OfficerIDCard = '2',
		PoliceIDCard = '3',
		SoldierIDCard = '4',
		HouseholdRegister = '5',
		Passport = '6',
		TaiwanCompatriotIDCard = '7',
		HomeComingCard = '8',
		LicenseNo = '9',
		TaxNo = 'A',
		OtherCard = 'x',
	}

	///TFtdcInvestorRangeType是一个投资者范围类型
	public enum InvestorRange 
	{
		All = '1',
		Group = '2',
		Single = '3',
	}

	///TFtdcDepartmentRangeType是一个投资者范围类型
	public enum DepartmentRange 
	{
		All = '1',
		Group = '2',
		Single = '3',
	}

	///TFtdcDataSyncStatusType是一个数据同步状态类型
	public enum DataSyncStatus 
	{
		Asynchronous = '1',
		Synchronizing = '2',
		Synchronized = '3',
	}

	///TFtdcBrokerDataSyncStatusType是一个经纪公司数据同步状态类型
	public enum BrokerDataSyncStatus 
	{
		Synchronized = '1',
		Synchronizing = '2',
	}

	///TFtdcExchangeConnectStatusType是一个交易所连接状态类型
	public enum ExchangeConnectStatus 
	{
		NoConnection = '1',
		QryInstrumentSent = '2',
		GotInformation = '9',
	}

	///TFtdcTraderConnectStatusType是一个交易所交易员连接状态类型
	public enum TraderConnectStatus 
	{
		NotConnected = '1',
		Connected = '2',
		QryInstrumentSent = '3',
		SubPrivateFlow = '4',
	}

	///TFtdcFunctionCodeType是一个功能代码类型
	public enum FunctionCode 
	{
		DataAsync = '1',
		ForceUserLogout = '2',
		UserPasswordUpdate = '3',
		BrokerPasswordUpdate = '4',
		InvestorPasswordUpdate = '5',
		OrderInsert = '6',
		OrderAction = '7',
		SyncSystemData = '8',
		SyncBrokerData = '9',
		BachSyncBrokerData = 'A',
		SuperQuery = 'B',
		ParkedOrderInsert = 'C',
		ParkedOrderAction = 'D',
		SyncOTP = 'E',
	}

	///TFtdcBrokerFunctionCodeType是一个经纪公司功能代码类型
	public enum BrokerFunctionCode 
	{
		ForceUserLogout = '1',
		UserPasswordUpdate = '2',
		SyncBrokerData = '3',
		BachSyncBrokerData = '4',
		OrderInsert = '5',
		OrderAction = '6',
		AllQuery = '7',
		log = 'a',
		BaseQry = 'b',
		TradeQry = 'c',
		Trade = 'd',
		Virement = 'e',
		Risk = 'f',
		Session = 'g',
		RiskNoticeCtl = 'h',
		RiskNotice = 'i',
		BrokerDeposit = 'j',
		QueryFund = 'k',
		QueryOrder = 'l',
		QueryTrade = 'm',
		QueryPosition = 'n',
		QueryMarketData = 'o',
		QueryUserEvent = 'p',
		QueryRiskNotify = 'q',
		QueryFundChange = 'r',
		QueryInvestor = 's',
		QueryTradingCode = 't',
		ForceClose = 'u',
		PressTest = 'v',
		RemainCalc = 'w',
		NetPositionInd = 'x',
		RiskPredict = 'y',
		DataExport = 'z',
		RiskTargetSetup = 'A',
		MarketDataWarn = 'B',
		QryBizNotice = 'C',
		CfgBizNotice = 'D',
		SyncOTP = 'E',
		SendBizNotice = 'F',
		CfgRiskLevelStd = 'G',
	}

	///TFtdcOrderActionStatusType是一个报单操作状态类型
	public enum OrderActionStatus 
	{
		Submitted = 'a',
		Accepted = 'b',
		Rejected = 'c',
	}

	///TFtdcOrderStatusType是一个报单状态类型
	public enum OrderStatus 
	{
		AllTraded = '0',
		PartTradedQueueing = '1',
		PartTradedNotQueueing = '2',
		NoTradeQueueing = '3',
		NoTradeNotQueueing = '4',
		Canceled = '5',
		Unknown = 'a',
		NotTouched = 'b',
		Touched = 'c',
	}

	///TFtdcOrderSubmitStatusType是一个报单提交状态类型
	public enum OrderSubmitStatus 
	{
		InsertSubmitted = '0',
		CancelSubmitted = '1',
		ModifySubmitted = '2',
		Accepted = '3',
		InsertRejected = '4',
		CancelRejected = '5',
		ModifyRejected = '6',
	}

	///TFtdcPositionDateType是一个持仓日期类型
	public enum PositionDate 
	{
		Today = '1',
		History = '2',
	}

	///TFtdcPositionDateTypeType是一个持仓日期类型类型
	public enum PositionDateType 
	{
		UseHistory = '1',
		NoUseHistory = '2',
	}

	///TFtdcTradingRoleType是一个交易角色类型
	public enum TradingRole 
	{
		Broker = '1',
		Host = '2',
		Maker = '3',
	}

	///TFtdcProductClassType是一个产品类型类型
	public enum ProductClass 
	{
		Futures = '1',
		Options = '2',
		Combination = '3',
		Spot = '4',
		EFP = '5',
	}

	///TFtdcInstLifePhaseType是一个合约生命周期状态类型
	public enum InstLifePhase 
	{
		NotStart = '0',
		Started = '1',
		Pause = '2',
		Expired = '3',
	}

	///TFtdcDirectionType是一个买卖方向类型
	public enum Direction 
	{
		Buy = '0',
		Sell = '1',
	}

	///TFtdcPositionTypeType是一个持仓类型类型
	public enum PositionType 
	{
		Net = '1',
		Gross = '2',
	}

	///TFtdcPosiDirectionType是一个持仓多空方向类型
	public enum PosiDirection 
	{
		Net = '1',
		Long = '2',
		Short = '3',
	}

	///TFtdcSysSettlementStatusType是一个系统结算状态类型
	public enum SysSettlementStatus 
	{
		NonActive = '1',
		Startup = '2',
		Operating = '3',
		Settlement = '4',
		SettlementFinished = '5',
	}

	///TFtdcRatioAttrType是一个费率属性类型
	public enum RatioAttr 
	{
		Trade = '0',
		Settlement = '1',
	}

	///TFtdcHedgeFlagType是一个投机套保标志类型
	public enum HedgeFlag 
	{
		Speculation = '1',
		Arbitrage = '2',
		Hedge = '3',
	}

	///TFtdcBillHedgeFlagType是一个投机套保标志类型
	public enum BillHedgeFlag 
	{
		Speculation = '1',
		Arbitrage = '2',
		Hedge = '3',
	}

	///TFtdcClientIDTypeType是一个交易编码类型类型
	public enum ClientIDType 
	{
		Speculation = '1',
		Arbitrage = '2',
		Hedge = '3',
	}

	///TFtdcOrderPriceTypeType是一个报单价格条件类型
	public enum OrderPriceType 
	{
		AnyPrice = '1',
		LimitPrice = '2',
		BestPrice = '3',
		LastPrice = '4',
		LastPricePlusOneTicks = '5',
		LastPricePlusTwoTicks = '6',
		LastPricePlusThreeTicks = '7',
		AskPrice1 = '8',
		AskPrice1PlusOneTicks = '9',
		AskPrice1PlusTwoTicks = 'A',
		AskPrice1PlusThreeTicks = 'B',
		BidPrice1 = 'C',
		BidPrice1PlusOneTicks = 'D',
		BidPrice1PlusTwoTicks = 'E',
		BidPrice1PlusThreeTicks = 'F',
	}

	///TFtdcOffsetFlagType是一个开平标志类型
	public enum OffsetFlag 
	{
		Open = '0',
		Close = '1',
		ForceClose = '2',
		CloseToday = '3',
		CloseYesterday = '4',
		ForceOff = '5',
		LocalForceClose = '6',
	}

	///TFtdcForceCloseReasonType是一个强平原因类型
	public enum ForceCloseReason 
	{
		NotForceClose = '0',
		LackDeposit = '1',
		ClientOverPositionLimit = '2',
		MemberOverPositionLimit = '3',
		NotMultiple = '4',
		Violation = '5',
		Other = '6',
		PersonDeliv = '7',
	}

	///TFtdcOrderTypeType是一个报单类型类型
	public enum OrderType 
	{
		Normal = '0',
		DeriveFromQuote = '1',
		DeriveFromCombination = '2',
		Combination = '3',
		ConditionalOrder = '4',
		Swap = '5',
	}

	///TFtdcTimeConditionType是一个有效期类型类型
	public enum TimeCondition 
	{
		IOC = '1',
		GFS = '2',
		GFD = '3',
		GTD = '4',
		GTC = '5',
		GFA = '6',
	}

	///TFtdcVolumeConditionType是一个成交量类型类型
	public enum VolumeCondition 
	{
		AV = '1',
		MV = '2',
		CV = '3',
	}

	///TFtdcContingentConditionType是一个触发条件类型
	public enum ContingentCondition 
	{
		Immediately = '1',
		Touch = '2',
		TouchProfit = '3',
		ParkedOrder = '4',
		LastPriceGreaterThanStopPrice = '5',
		LastPriceGreaterEqualStopPrice = '6',
		LastPriceLesserThanStopPrice = '7',
		LastPriceLesserEqualStopPrice = '8',
		AskPriceGreaterThanStopPrice = '9',
		AskPriceGreaterEqualStopPrice = 'A',
		AskPriceLesserThanStopPrice = 'B',
		AskPriceLesserEqualStopPrice = 'C',
		BidPriceGreaterThanStopPrice = 'D',
		BidPriceGreaterEqualStopPrice = 'E',
		BidPriceLesserThanStopPrice = 'F',
		BidPriceLesserEqualStopPrice = 'H',
	}

	///TFtdcActionFlagType是一个操作标志类型
	public enum ActionFlag 
	{
		Delete = '0',
		Modify = '3',
	}

	///TFtdcTradingRightType是一个交易权限类型
	public enum TradingRight 
	{
		Allow = '0',
		CloseOnly = '1',
		Forbidden = '2',
	}

	///TFtdcOrderSourceType是一个报单来源类型
	public enum OrderSource 
	{
		Participant = '0',
		Administrator = '1',
	}

	///TFtdcTradeTypeType是一个成交类型类型
	public enum TradeType 
	{
		Common = '0',
		OptionsExecution = '1',
		OTC = '2',
		EFPDerived = '3',
		CombinationDerived = '4',
	}

	///TFtdcPriceSourceType是一个成交价来源类型
	public enum PriceSource 
	{
		LastPrice = '0',
		Buy = '1',
		Sell = '2',
	}

	///TFtdcInstrumentStatusType是一个合约交易状态类型
	public enum InstrumentStatus 
	{
		BeforeTrading = '0',
		NoTrading = '1',
		Continous = '2',
		AuctionOrdering = '3',
		AuctionBalance = '4',
		AuctionMatch = '5',
		Closed = '6',
	}

	///TFtdcInstStatusEnterReasonType是一个品种进入交易状态原因类型
	public enum InstStatusEnterReason 
	{
		Automatic = '1',
		Manual = '2',
		Fuse = '3',
	}

	///TFtdcBatchStatusType是一个处理状态类型
	public enum BatchStatus 
	{
		NoUpload = '1',
		Uploaded = '2',
		Failed = '3',
	}

	///TFtdcReturnStyleType是一个按品种返还方式类型
	public enum ReturnStyle 
	{
		All = '1',
		ByProduct = '2',
	}

	///TFtdcReturnPatternType是一个返还模式类型
	public enum ReturnPattern 
	{
		ByVolume = '1',
		ByFeeOnHand = '2',
	}

	///TFtdcReturnLevelType是一个返还级别类型
	public enum ReturnLevel 
	{
		Level1 = '1',
		Level2 = '2',
		Level3 = '3',
		Level4 = '4',
		Level5 = '5',
		Level6 = '6',
		Level7 = '7',
		Level8 = '8',
		Level9 = '9',
	}

	///TFtdcReturnStandardType是一个返还标准类型
	public enum ReturnStandard 
	{
		ByPeriod = '1',
		ByStandard = '2',
	}

	///TFtdcMortgageTypeType是一个质押类型类型
	public enum MortgageType 
	{
		Out = '0',
		In = '1',
	}

	///TFtdcInvestorSettlementParamIDType是一个投资者结算参数代码类型
	public enum InvestorSettlementParamID 
	{
		BaseMargin = '1',
		LowestInterest = '2',
		MortgageRatio = '4',
		MarginWay = '5',
		BillDeposit = '9',
	}

	///TFtdcExchangeSettlementParamIDType是一个交易所结算参数代码类型
	public enum ExchangeSettlementParamID 
	{
		MortgageRatio = '1',
		OtherFundItem = '2',
		OtherFundImport = '3',
		SHFEDelivFee = '4',
		DCEDelivFee = '5',
		CFFEXMinPrepa = '6',
	}

	///TFtdcSystemParamIDType是一个系统参数代码类型
	public enum SystemParamID 
	{
		InvestorIDMinLength = '1',
		AccountIDMinLength = '2',
		UserRightLogon = '3',
		SettlementBillTrade = '4',
		TradingCode = '5',
		CheckFund = '6',
		CommModelRight = '7',
		IsStandardActive = '8',
		UploadSettlementFile = 'U',
		DownloadCSRCFile = 'D',
		SettlementBillFile = 'S',
		CSRCOthersFile = 'C',
		InvestorPhoto = 'P',
		CSRCData = 'R',
		InvestorPwdModel = 'I',
		CFFEXInvestorSettleFile = 'F',
		InvestorIDType = 'a',
	}

	///TFtdcTradeParamIDType是一个交易系统参数代码类型
	public enum TradeParamID 
	{
		EncryptionStandard = 'E',
		RiskMode = 'R',
		RiskModeGlobal = 'G',
	}

	///TFtdcFileIDType是一个文件标识类型
	public enum FileID 
	{
		SettlementFund = 'F',
		Trade = 'T',
		InvestorPosition = 'P',
		SubEntryFund = 'O',
		CZCECombinationPos = 'C',
		CSRCData = 'R',
	}

	///TFtdcFileTypeType是一个文件上传类型类型
	public enum FileType 
	{
		Settlement = '0',
		Check = '1',
	}

	///TFtdcFileFormatType是一个文件格式类型
	public enum FileFormat 
	{
		Txt = '0',
		Zip = '1',
		DBF = '2',
	}

	///TFtdcFileUploadStatusType是一个文件状态类型
	public enum FileUploadStatus 
	{
		SucceedUpload = '1',
		FailedUpload = '2',
		SucceedLoad = '3',
		PartSucceedLoad = '4',
		FailedLoad = '5',
	}

	///TFtdcTransferDirectionType是一个移仓方向类型
	public enum TransferDirection 
	{
		Out = '0',
		In = '1',
	}

	///TFtdcBankFlagType是一个银行统一标识类型类型
	public enum BankFlag 
	{
		ICBC = '1',
		ABC = '2',
		BC = '3',
		CBC = '4',
		BOC = '5',
		Other = 'Z',
	}

	///TFtdcSpecialCreateRuleType是一个特殊的创建规则类型
	public enum SpecialCreateRule 
	{
		NoSpecialRule = '0',
		NoSpringFestival = '1',
	}

	///TFtdcBasisPriceTypeType是一个挂牌基准价类型类型
	public enum BasisPriceType 
	{
		LastSettlement = '1',
		LaseClose = '2',
	}

	///TFtdcProductLifePhaseType是一个产品生命周期状态类型
	public enum ProductLifePhase 
	{
		Active = '1',
		NonActive = '2',
		Canceled = '3',
	}

	///TFtdcDeliveryModeType是一个交割方式类型
	public enum DeliveryMode 
	{
		CashDeliv = '1',
		CommodityDeliv = '2',
	}

	///TFtdcFundIOTypeType是一个出入金类型类型
	public enum FundIOType 
	{
		FundIO = '1',
		Transfer = '2',
	}

	///TFtdcFundTypeType是一个资金类型类型
	public enum FundType 
	{
		Deposite = '1',
		ItemFund = '2',
		Company = '3',
	}

	///TFtdcFundDirectionType是一个出入金方向类型
	public enum FundDirection 
	{
		In = '1',
		Out = '2',
	}

	///TFtdcFundStatusType是一个资金状态类型
	public enum FundStatus 
	{
		Record = '1',
		Check = '2',
		Charge = '3',
	}

	///TFtdcPublishStatusType是一个发布状态类型
	public enum PublishStatus 
	{
		None = '1',
		Publishing = '2',
		Published = '3',
	}

	///TFtdcSystemStatusType是一个系统状态类型
	public enum SystemStatus 
	{
		NonActive = '1',
		Startup = '2',
		Initialize = '3',
		Initialized = '4',
		Close = '5',
		Closed = '6',
		Settlement = '7',
	}

	///TFtdcSettlementStatusType是一个结算状态类型
	public enum SettlementStatus 
	{
		Initialize = '0',
		Settlementing = '1',
		Settlemented = '2',
		Finished = '3',
	}

	///TFtdcInvestorTypeType是一个投资者类型类型
	public enum InvestorType 
	{
		Person = '0',
		Company = '1',
		Fund = '2',
	}

	///TFtdcBrokerTypeType是一个经纪公司类型类型
	public enum BrokerType 
	{
		Trade = '0',
		TradeSettle = '1',
	}

	///TFtdcRiskLevelType是一个风险等级类型
	public enum RiskLevel 
	{
		Low = '1',
		Normal = '2',
		Focus = '3',
		Risk = '4',
	}

	///TFtdcFeeAcceptStyleType是一个手续费收取方式类型
	public enum FeeAcceptStyle 
	{
		ByTrade = '1',
		ByDeliv = '2',
		None = '3',
		FixFee = '4',
	}

	///TFtdcPasswordTypeType是一个密码类型类型
	public enum PasswordType 
	{
		Trade = '1',
		Account = '2',
	}

	///TFtdcAlgorithmType是一个盈亏算法类型
	public enum Algorithm 
	{
		All = '1',
		OnlyLost = '2',
		OnlyGain = '3',
		None = '4',
	}

	///TFtdcIncludeCloseProfitType是一个是否包含平仓盈利类型
	public enum IncludeCloseProfit 
	{
		Include = '0',
		NotInclude = '2',
	}

	///TFtdcAllWithoutTradeType是一个是否受可提比例限制类型
	public enum AllWithoutTrade 
	{
		Enable = '0',
		Disable = '2',
	}

	///TFtdcFuturePwdFlagType是一个资金密码核对标志类型
	public enum FuturePwdFlag 
	{
		UnCheck = '0',
		Check = '1',
	}

	///TFtdcTransferTypeType是一个银期转账类型类型
	public enum TransferType 
	{
		BankToFuture = '0',
		FutureToBank = '1',
	}

	///TFtdcTransferValidFlagType是一个转账有效标志类型
	public enum TransferValidFlag 
	{
		Invalid = '0',
		Valid = '1',
		Reverse = '2',
	}

	///TFtdcReasonType是一个事由类型
	public enum Reason 
	{
		CD = '0',
		ZT = '1',
		QT = '2',
	}

	///TFtdcSexType是一个性别类型
	public enum Sex 
	{
		None = '0',
		Man = '1',
		Woman = '2',
	}

	///TFtdcUserTypeType是一个用户类型类型
	public enum UserType 
	{
		Investor = '0',
		Operator = '1',
		SuperUser = '2',
	}

	///TFtdcRateTypeType是一个费率类型类型
	public enum RateType 
	{
		MarginRate = '2',
	}

	///TFtdcNoteTypeType是一个通知类型类型
	public enum NoteType 
	{
		TradeSettleBill = '1',
		TradeSettleMonth = '2',
		CallMarginNotes = '3',
		ForceCloseNotes = '4',
		TradeNotes = '5',
		DelivNotes = '6',
	}

	///TFtdcSettlementStyleType是一个结算单方式类型
	public enum SettlementStyle 
	{
		Day = '1',
		Volume = '2',
	}

	///TFtdcSettlementBillTypeType是一个结算单类型类型
	public enum SettlementBillType 
	{
		Day = '0',
		Month = '1',
	}

	///TFtdcUserRightTypeType是一个客户权限类型类型
	public enum UserRightType 
	{
		Logon = '1',
		Transfer = '2',
		EMail = '3',
		Fax = '4',
		ConditionOrder = '5',
	}

	///TFtdcMarginPriceTypeType是一个保证金价格类型类型
	public enum MarginPriceType 
	{
		PreSettlementPrice = '1',
		SettlementPrice = '2',
		AveragePrice = '3',
		OpenPrice = '4',
	}

	///TFtdcBillGenStatusType是一个结算单生成状态类型
	public enum BillGenStatus 
	{
		None = '0',
		NoGenerated = '1',
		Generated = '2',
	}

	///TFtdcAlgoTypeType是一个算法类型类型
	public enum AlgoType 
	{
		HandlePositionAlgo = '1',
		FindMarginRateAlgo = '2',
	}

	///TFtdcHandlePositionAlgoIDType是一个持仓处理算法编号类型
	public enum HandlePositionAlgoID 
	{
		Base = '1',
		DCE = '2',
		CZCE = '3',
	}

	///TFtdcFindMarginRateAlgoIDType是一个寻找保证金率算法编号类型
	public enum FindMarginRateAlgoID 
	{
		Base = '1',
		DCE = '2',
		CZCE = '3',
	}

	///TFtdcHandleTradingAccountAlgoIDType是一个资金处理算法编号类型
	public enum HandleTradingAccountAlgoID 
	{
		Base = '1',
		DCE = '2',
		CZCE = '3',
	}

	///TFtdcPersonTypeType是一个联系人类型类型
	public enum PersonType 
	{
		Order = '1',
		Open = '2',
		Fund = '3',
		Settlement = '4',
		Company = '5',
		Corporation = '6',
		LinkMan = '7',
	}

	///TFtdcQueryInvestorRangeType是一个查询范围类型
	public enum QueryInvestorRange 
	{
		All = '1',
		Group = '2',
		Single = '3',
	}

	///TFtdcInvestorRiskStatusType是一个投资者风险状态类型
	public enum InvestorRiskStatus 
	{
		Normal = '1',
		Warn = '2',
		Call = '3',
		Force = '4',
		Exception = '5',
	}

	///TFtdcUserEventTypeType是一个用户事件类型类型
	public enum UserEventType 
	{
		Login = '1',
		Logout = '2',
		Trading = '3',
		TradingError = '4',
		UpdatePassword = '5',
		Authenticate = '6',
		Other = '9',
	}

	///TFtdcCloseStyleType是一个平仓方式类型
	public enum CloseStyle 
	{
		Close = '0',
		CloseToday = '1',
	}

	///TFtdcStatModeType是一个统计方式类型
	public enum StatMode 
	{
		Non = '0',
		Instrument = '1',
		Product = '2',
		Investor = '3',
	}

	///TFtdcParkedOrderStatusType是一个预埋单状态类型
	public enum ParkedOrderStatus 
	{
		NotSend = '1',
		Send = '2',
		Deleted = '3',
	}

	///TFtdcVirDealStatusType是一个处理状态类型
	public enum VirDealStatus 
	{
		Dealing = '1',
		DeaclSucceed = '2',
	}

	///TFtdcOrgSystemIDType是一个原有系统代码类型
	public enum OrgSystemID 
	{
		Standard = '0',
		ESunny = '1',
		KingStarV6 = '2',
	}

	///TFtdcVirTradeStatusType是一个交易状态类型
	public enum VirTradeStatus 
	{
		NaturalDeal = '0',
		SucceedEnd = '1',
		FailedEND = '2',
		Exception = '3',
		ManualDeal = '4',
		MesException = '5',
		SysException = '6',
	}

	///TFtdcVirBankAccTypeType是一个银行帐户类型类型
	public enum VirBankAccType 
	{
		BankBook = '1',
		BankCard = '2',
		CreditCard = '3',
	}

	///TFtdcVirementStatusType是一个银行帐户类型类型
	public enum VirementStatus 
	{
		Natural = '0',
		Canceled = '9',
	}

	///TFtdcVirementAvailAbilityType是一个有效标志类型
	public enum VirementAvailAbility 
	{
		NoAvailAbility = '0',
		AvailAbility = '1',
		Repeal = '2',
	}

	///TFtdcAMLGenStatusType是一个Aml生成方式类型
	public enum AMLGenStatus 
	{
		Program = '0',
		HandWork = '1',
	}

	///TFtdcCFMMCKeyType是一个密钥类型(保证金监管)类型
	public enum CFMMCKeyKind 
	{
		REQUEST = 'R',
		AUTO = 'A',
		MANUAL = 'M',
	}

	///TFtdcCertificationTypeType是一个证件类型类型
	public enum CertificationType 
	{
		IDCard = '0',
		Passport = '1',
		OfficerIDCard = '2',
		SoldierIDCard = '3',
		HomeComingCard = '4',
		HouseholdRegister = '5',
		LicenseNo = '6',
		InstitutionCodeCard = '7',
		TempLicenseNo = '8',
		NoEnterpriseLicenseNo = '9',
		OtherCard = 'x',
		SuperDepAgree = 'a',
	}

	///TFtdcFileBusinessCodeType是一个文件业务功能类型
	public enum FileBusinessCode 
	{
		Others = '0',
		TransferDetails = '1',
		CustAccStatus = '2',
		AccountTradeDetails = '3',
		FutureAccountChangeInfoDetails = '4',
		CustMoneyDetail = '5',
		CustCancelAccountInfo = '6',
		CustMoneyResult = '7',
		OthersExceptionResult = '8',
		CustInterestNetMoneyDetails = '9',
		CustMoneySendAndReceiveDetails = 'a',
		CorporationMoneyTotal = 'b',
		MainbodyMoneyTotal = 'c',
		MainPartMonitorData = 'd',
		PreparationMoney = 'e',
		BankMoneyMonitorData = 'f',
	}

	///TFtdcCashExchangeCodeType是一个汇钞标志类型
	public enum CashExchangeCode 
	{
		Exchange = '1',
		Cash = '2',
	}

	///TFtdcYesNoIndicatorType是一个是或否标识类型
	public enum YesNoIndicator 
	{
		Yes = '0',
		No = '1',
	}

	///TFtdcBanlanceTypeType是一个余额类型类型
	public enum BanlanceType 
	{
		CurrentMoney = '0',
		UsableMoney = '1',
		FetchableMoney = '2',
		FreezeMoney = '3',
	}

	///TFtdcGenderType是一个性别类型
	public enum Gender 
	{
		Unknown = '0',
		Male = '1',
		Female = '2',
	}

	///TFtdcFeePayFlagType是一个费用支付标志类型
	public enum FeePayFlag 
	{
		BEN = '0',
		OUR = '1',
		SHA = '2',
	}

	///TFtdcPassWordKeyTypeType是一个密钥类型类型
	public enum PassWordKeyType 
	{
		ExchangeKey = '0',
		PassWordKey = '1',
		MACKey = '2',
		MessageKey = '3',
	}

	///TFtdcFBTPassWordTypeType是一个密码类型类型
	public enum FBTPassWordType 
	{
		Query = '0',
		Fetch = '1',
		Transfer = '2',
		Trade = '3',
	}

	///TFtdcFBTEncryModeType是一个加密方式类型
	public enum FBTEncryMode 
	{
		NoEncry = '0',
		DES = '1',
		_3DES = '2',
	}

	///TFtdcBankRepealFlagType是一个银行冲正标志类型
	public enum BankRepealFlag 
	{
		BankNotNeedRepeal = '0',
		BankWaitingRepeal = '1',
		BankBeenRepealed = '2',
	}

	///TFtdcBrokerRepealFlagType是一个期商冲正标志类型
	public enum BrokerRepealFlag 
	{
		BrokerNotNeedRepeal = '0',
		BrokerWaitingRepeal = '1',
		BrokerBeenRepealed = '2',
	}

	///TFtdcInstitutionTypeType是一个机构类别类型
	public enum InstitutionType 
	{
		Bank = '0',
		Future = '1',
		Store = '2',
	}

	///TFtdcLastFragmentType是一个最后分片标志类型
	public enum LastFragment 
	{
		Yes = '0',
		No = '1',
	}

	///TFtdcBankAccStatusType是一个银行账户状态类型
	public enum BankAccStatus 
	{
		Normal = '0',
		Freeze = '1',
		ReportLoss = '2',
	}

	///TFtdcMoneyAccountStatusType是一个资金账户状态类型
	public enum MoneyAccountStatus 
	{
		Normal = '0',
		Cancel = '1',
	}

	///TFtdcManageStatusType是一个存管状态类型
	public enum ManageStatus 
	{
		Point = '0',
		PrePoint = '1',
		CancelPoint = '2',
	}

	///TFtdcSystemTypeType是一个应用系统类型类型
	public enum SystemType 
	{
		FutureBankTransfer = '0',
		StockBankTransfer = '1',
		TheThirdPartStore = '2',
	}

	///TFtdcTxnEndFlagType是一个银期转帐划转结果标志类型
	public enum TxnEndFlag 
	{
		NormalProcessing = '0',
		Success = '1',
		Failed = '2',
		Abnormal = '3',
		ManualProcessedForException = '4',
		CommuFailedNeedManualProcess = '5',
		SysErrorNeedManualProcess = '6',
	}

	///TFtdcProcessStatusType是一个银期转帐服务处理状态类型
	public enum ProcessStatus 
	{
		NotProcess = '0',
		StartProcess = '1',
		Finished = '2',
	}

	///TFtdcCustTypeType是一个客户类型类型
	public enum CustType 
	{
		Person = '0',
		Institution = '1',
	}

	///TFtdcFBTTransferDirectionType是一个银期转帐方向类型
	public enum FBTTransferDirection 
	{
		FromBankToFuture = '1',
		FromFutureToBank = '2',
	}

	///TFtdcOpenOrDestroyType是一个开销户类别类型
	public enum OpenOrDestroy 
	{
		Open = '1',
		Destroy = '0',
	}

	///TFtdcAvailabilityFlagType是一个有效标志类型
	public enum AvailabilityFlag 
	{
		Invalid = '0',
		Valid = '1',
		Repeal = '2',
	}

	///TFtdcOrganTypeType是一个机构类型类型
	public enum OrganType 
	{
		Bank = '1',
		Future = '2',
		PlateForm = '9',
	}

	///TFtdcOrganLevelType是一个机构级别类型
	public enum OrganLevel 
	{
		HeadQuarters = '1',
		Branch = '2',
	}

	///TFtdcProtocalIDType是一个协议类型类型
	public enum ProtocalID 
	{
		FutureProtocal = '0',
		ICBCProtocal = '1',
		ABCProtocal = '2',
		CBCProtocal = '3',
		CCBProtocal = '4',
		BOCOMProtocal = '5',
		FBTPlateFormProtocal = 'X',
	}

	///TFtdcConnectModeType是一个套接字连接方式类型
	public enum ConnectMode 
	{
		ShortConnect = '0',
		LongConnect = '1',
	}

	///TFtdcSyncModeType是一个套接字通信方式类型
	public enum SyncMode 
	{
		ASync = '0',
		Sync = '1',
	}

	///TFtdcBankAccTypeType是一个银行帐号类型类型
	public enum BankAccType 
	{
		BankBook = '1',
		SavingCard = '2',
		CreditCard = '3',
	}

	///TFtdcFutureAccTypeType是一个期货公司帐号类型类型
	public enum FutureAccType 
	{
		BankBook = '1',
		SavingCard = '2',
		CreditCard = '3',
	}

	///TFtdcOrganStatusType是一个接入机构状态类型
	public enum OrganStatus 
	{
		Ready = '0',
		CheckIn = '1',
		CheckOut = '2',
		CheckFileArrived = '3',
		CheckDetail = '4',
		DayEndClean = '5',
		Invalid = '9',
	}

	///TFtdcCCBFeeModeType是一个建行收费模式类型
	public enum CCBFeeMode 
	{
		ByAmount = '1',
		ByMonth = '2',
	}

	///TFtdcCommApiTypeType是一个通讯API类型类型
	public enum CommApiType 
	{
		Client = '1',
		Server = '2',
		UserApi = '3',
	}

	///TFtdcLinkStatusType是一个连接状态类型
	public enum LinkStatus 
	{
		Connected = '1',
		Disconnected = '2',
	}

	///TFtdcPwdFlagType是一个密码核对标志类型
	public enum PwdFlag 
	{
		NoCheck = '0',
		BlankCheck = '1',
		EncryptCheck = '2',
	}

	///TFtdcSecuAccTypeType是一个期货帐号类型类型
	public enum SecuAccType 
	{
		AccountID = '1',
		CardID = '2',
		SHStockholderID = '3',
		SZStockholderID = '4',
	}

	///TFtdcTransferStatusType是一个转账交易状态类型
	public enum TransferStatus 
	{
		Normal = '0',
		Repealed = '1',
	}

	///TFtdcSponsorTypeType是一个发起方类型
	public enum SponsorType 
	{
		Broker = '0',
		Bank = '1',
	}

	///TFtdcReqRspTypeType是一个请求响应类别类型
	public enum ReqRspType 
	{
		Request = '0',
		Response = '1',
	}

	///TFtdcFBTUserEventTypeType是一个银期转帐用户事件类型类型
	public enum FBTUserEventType 
	{
		SignIn = '0',
		FromBankToFuture = '1',
		FromFutureToBank = '2',
		OpenAccount = '3',
		CancelAccount = '4',
		ChangeAccount = '5',
		RepealFromBankToFuture = '6',
		RepealFromFutureToBank = '7',
		QueryBankAccount = '8',
		QueryFutureAccount = '9',
		SignOut = 'A',
		SyncKey = 'B',
		Other = 'Z',
	}

	///TFtdcNotifyClassType是一个风险通知类型类型
	public enum NotifyClass 
	{
		NOERROR = '0',
		Warn = '1',
		Call = '2',
		Force = '3',
		CHUANCANG = '4',
		Exception = '5',
	}

	///TFtdcForceCloseTypeType是一个强平单类型类型
	public enum ForceCloseType 
	{
		Manual = '0',
		Single = '1',
		Group = '2',
	}

	///TFtdcRiskNotifyMethodType是一个风险通知途径类型
	public enum RiskNotifyMethod 
	{
		System = '0',
		SMS = '1',
		EMail = '2',
		Manual = '3',
	}

	///TFtdcRiskNotifyStatusType是一个风险通知状态类型
	public enum RiskNotifyStatus 
	{
		NotGen = '0',
		Generated = '1',
		SendError = '2',
		SendOk = '3',
		Received = '4',
		Confirmed = '5',
	}

	///TFtdcRiskUserEventType是一个风控用户操作事件类型
	public enum RiskUserEvent 
	{
		ExportData = '0',
	}

	///TFtdcConditionalOrderSortTypeType是一个条件单索引条件类型
	public enum ConditionalOrderSortType 
	{
		LastPriceAsc = '0',
		LastPriceDesc = '1',
		AskPriceAsc = '2',
		AskPriceDesc = '3',
		BidPriceAsc = '4',
		BidPriceDesc = '5',
	}

	///TFtdcSendTypeType是一个报送状态类型
	public enum SendType 
	{
		NoSend = '0',
		Sended = '1',
		Generated = '2',
		SendFail = '3',
		Success = '4',
		Fail = '5',
		Cancel = '6',
	}

	///TFtdcClientIDStatusType是一个交易编码状态类型
	public enum ClientIDStatus 
	{
		NoApply = '1',
		Submited = '2',
		Sended = '3',
		Success = '4',
		Refuse = '5',
		Cancel = '6',
	}

	///TFtdcQuestionTypeType是一个特有信息类型类型
	public enum QuestionType 
	{
		Radio = '1',
		Option = '2',
		Blank = '3',
	}

	///TFtdcProcessTypeType是一个流程功能类型类型
	public enum ProcessType 
	{
		ApplyTradingCode = '1',
		CancelTradingCode = '2',
		ModifyIDCard = '3',
		ModifyNoIDCard = '4',
		ExchOpenBak = '5',
		ExchCancelBak = '6',
		StandardAccount = '7',
		FreezeAccount = '8',
		ActiveFreezeAccount = '9',
	}

	///TFtdcBusinessTypeType是一个业务类型类型
	public enum BusinessType 
	{
		Request = '1',
		Response = '2',
		Notice = '3',
	}

	///TFtdcCfmmcReturnCodeType是一个监控中心返回码类型
	public enum CfmmcReturnCode 
	{
		Success = '0',
		Working = '1',
		InfoFail = '2',
		IDCardFail = '3',
		OtherFail = '4',
	}

	///TFtdcClientTypeType是一个客户类型类型
	public enum ClientType 
	{
		All = '0',
		Person = '1',
		Company = '2',
	}

	///TFtdcExchangeIDTypeType是一个交易所编号类型
	public enum ExchangeIDType 
	{
		SHFE = 'S',
		CZCE = 'Z',
		DCE = 'D',
		CFFEX = 'J',
	}

	///TFtdcExClientIDTypeType是一个交易编码类型类型
	public enum ExClientIDType 
	{
		Hedge = '1',
		Arbitrage = '2',
		Speculation = '3',
	}

	///TFtdcUpdateFlagType是一个更新状态类型
	public enum UpdateFlag 
	{
		NoUpdate = '0',
		Success = '1',
		Fail = '2',
		TCSuccess = '3',
		TCFail = '4',
		Cancel = '5',
	}

	///TFtdcApplyOperateIDType是一个申请动作类型
	public enum ApplyOperateID 
	{
		OpenInvestor = '1',
		ModifyIDCard = '2',
		ModifyNoIDCard = '3',
		ApplyTradingCode = '4',
		CancelTradingCode = '5',
		CancelInvestor = '6',
		FreezeAccount = '8',
		ActiveFreezeAccount = '9',
	}

	///TFtdcApplyStatusIDType是一个申请状态类型
	public enum ApplyStatusID 
	{
		NoComplete = '1',
		Submited = '2',
		Checked = '3',
		Refused = '4',
		Deleted = '5',
	}

	///TFtdcSendMethodType是一个发送方式类型
	public enum SendMethod 
	{
		ByAPI = '1',
		ByFile = '2',
	}

	///TFtdcEventModeType是一个操作方法类型
	public enum EventMode 
	{
		ADD = '1',
		UPDATE = '2',
		DELETE = '3',
		CHECK = '4',
		COPY = '5',
		CANCEL = '6',
		Reverse = '7',
	}

	///TFtdcUOAAutoSendType是一个统一开户申请自动发送类型
	public enum UOAAutoSend 
	{
		ASR = '1',
		ASNR = '2',
		NSAR = '3',
		NSR = '4',
	}

	///TFtdcFlowIDType是一个流程ID类型
	public enum FlowID 
	{
		InvestorGroupFlow = '1',
		InvestorRate = '2',
		InvestorCommRateModel = '3',
	}

	///TFtdcCheckLevelType是一个复核级别类型
	public enum CheckLevel 
	{
		Zero = '0',
		One = '1',
		Two = '2',
	}

	///TFtdcCheckStatusType是一个复核级别类型
	public enum CheckStatus 
	{
		Init = '0',
		Checking = '1',
		Checked = '2',
		Refuse = '3',
		Cancel = '4',
	}

	///TFtdcUsedStatusType是一个生效状态类型
	public enum UsedStatus 
	{
		Unused = '0',
		Used = '1',
		Fail = '2',
	}

	///TFtdcBankAcountOriginType是一个账户来源类型
	public enum BankAcountOrigin 
	{
		ByAccProperty = '0',
		ByFBTransfer = '1',
	}

	///TFtdcMonthBillTradeSumType是一个结算单月报成交汇总方式类型
	public enum MonthBillTradeSum 
	{
		ByInstrument = '0',
		ByDayInsPrc = '1',
		ByDayIns = '2',
	}

	///TFtdcOTPTypeType是一个动态令牌类型类型
	public enum OTPType 
	{
		NONE = '0',
		TOTP = '1',
	}

	///TFtdcOTPStatusType是一个动态令牌状态类型
	public enum OTPStatus 
	{
		Unused = '0',
		Used = '1',
		Disuse = '2',
	}

	///TFtdcBrokerUserTypeType是一个经济公司用户类型类型
	public enum BrokerUserType 
	{
		Investor = '1',
		BrokerUser = '2',
	}

	///TFtdcFutureTypeType是一个期货类型类型
	public enum FutureType 
	{
		Commodity = '1',
		Financial = '2',
	}

	///TFtdcFundEventTypeType是一个资金管理操作类型类型
	public enum FundEventType 
	{
		Restriction = '0',
		TodayRestriction = '1',
		Transfer = '2',
		Credit = '3',
		InvestorWithdrawAlm = '4',
		BankRestriction = '5',
		Accountregister = '6',
		ExchangeFundIO = '7',
		InvestorFundIO = '8',
	}

	///TFtdcAccountSourceTypeType是一个资金账户来源类型
	public enum AccountSourceType 
	{
		FBTransfer = '0',
		ManualEntry = '1',
	}

	///TFtdcCodeSourceTypeType是一个交易编码来源类型
	public enum CodeSourceType 
	{
		UnifyAccount = '0',
		ManualEntry = '1',
	}

	///TFtdcUserRangeType是一个操作员范围类型
	public enum UserRange 
	{
		All = '0',
		Single = '1',
	}

	///TFtdcByGroupType是一个交易统计表按客户统计方式类型
	public enum ByGroup 
	{
		Investor = '2',
		Group = '1',
	}

	///TFtdcTradeSumStatModeType是一个交易统计表按范围统计方式类型
	public enum TradeSumStatMode 
	{
		Instrument = '1',
		Product = '2',
		Exchange = '3',
	}

	///TFtdcRateInvestorRangeType是一个投资者范围类型
	public enum RateInvestorRange 
	{
		All = '1',
		Model = '2',
		Single = '3',
	}

	///TFtdcSyncDataStatusType是一个主次用系统数据同步状态类型
	public enum SyncDataStatus 
	{
		Initialize = '0',
		Settlementing = '1',
		Settlemented = '2',
	}

	///TFtdcTradeSourceType是一个成交来源类型
	public enum TradeSource 
	{
		NORMAL = '0',
		QUERY = '1',
	}

	///TFtdcFlexStatModeType是一个产品合约统计方式类型
	public enum FlexStatMode 
	{
		Product = '1',
		Exchange = '2',
		All = '3',
	}

	///TFtdcByInvestorRangeType是一个投资者范围统计方式类型
	public enum ByInvestorRange 
	{
		Property = '1',
		All = '2',
	}

	///TFtdcPropertyInvestorRangeType是一个投资者范围类型
	public enum PropertyInvestorRange 
	{
		All = '1',
		Property = '2',
		Single = '3',
	}

	///TFtdcFileStatusType是一个文件状态类型
	public enum FileStatus 
	{
		NoCreate = '0',
		Created = '1',
		Failed = '2',
	}

	///TFtdcFileGenStyleType是一个文件生成方式类型
	public enum FileGenStyle 
	{
		FileTransmit = '0',
		FileGen = '1',
	}

	///TFtdcSysOperModeType是一个系统日志操作方法类型
	public enum SysOperMode 
	{
		Add = '1',
		Update = '2',
		Delete = '3',
		Copy = '4',
		AcTive = '5',
		CanCel = '6',
		ReSet = '7',
	}

	///TFtdcSysOperTypeType是一个系统日志操作类型类型
	public enum SysOperType 
	{
		UpdatePassword = '0',
		UserDepartment = '1',
		RoleManager = '2',
		RoleFunction = '3',
		BaseParam = '4',
		SetUserID = '5',
		SetUserRole = '6',
		UserIpRestriction = '7',
		DepartmentManager = '8',
		DepartmentCopy = '9',
		Tradingcode = 'A',
		InvestorStatus = 'B',
		InvestorAuthority = 'C',
		PropertySet = 'D',
		ReSetInvestorPasswd = 'E',
		InvestorPersonalityInfo = 'F',
	}

	///TFtdcCSRCDataQueyTypeType是一个上报数据查询类型类型
	public enum CSRCDataQueyType 
	{
		Current = '0',
		History = '1',
	}

	///TFtdcFreezeStatusType是一个休眠状态类型
	public enum FreezeStatus 
	{
		Normal = '1',
		Freeze = '0',
	}

	///TFtdcStandardStatusType是一个规范状态类型
	public enum StandardStatus 
	{
		Standard = '0',
		NonStandard = '1',
	}

	///TFtdcRightParamTypeType是一个配置类型类型
	public enum RightParamType 
	{
		Freeze = '1',
		FreezeActive = '2',
	}

	///TFtdcDataStatusType是一个反洗钱审核表数据状态类型
	public enum DataStatus 
	{
		Normal = '0',
		Deleted = '1',
	}

	///TFtdcAMLCheckStatusType是一个审核状态类型
	public enum AMLCheckStatus 
	{
		Init = '0',
		Checking = '1',
		Checked = '2',
		RefuseReport = '3',
	}

	///TFtdcAmlDateTypeType是一个日期类型类型
	public enum AmlDateType 
	{
		DrawDay = '0',
		TouchDay = '1',
	}

	///TFtdcAmlCheckLevelType是一个审核级别类型
	public enum AmlCheckLevel 
	{
		CheckLevel0 = '0',
		CheckLevel1 = '1',
		CheckLevel2 = '2',
		CheckLevel3 = '3',
	}

	///TFtdcExportFileTypeType是一个导出文件类型类型
	public enum ExportFileType 
	{
		CSV = '0',
		EXCEL = '1',
		DBF = '2',
	}

	///TFtdcSettleManagerTypeType是一个结算配置类型类型
	public enum SettleManagerType 
	{
		Before = '1',
		Settlement = '2',
		After = '3',
		Settlemented = '4',
	}

	///TFtdcSettleManagerLevelType是一个结算配置等级类型
	public enum SettleManagerLevel 
	{
		Must = '1',
		Alarm = '2',
		Prompt = '3',
		Ignore = '4',
	}

	///TFtdcSettleManagerGroupType是一个模块分组类型
	public enum SettleManagerGroup 
	{
		Exhcange = '1',
		ASP = '2',
		CSRC = '3',
	}

	///TFtdcLimitUseTypeType是一个保值额度使用类型类型
	public enum LimitUseType 
	{
		Repeatable = '1',
		Unrepeatable = '2',
	}

	///TFtdcDataResourceType是一个数据来源类型
	public enum DataResource 
	{
		Settle = '1',
		Exchange = '2',
		CSRC = '3',
	}

}