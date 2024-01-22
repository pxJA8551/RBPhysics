#include "pch-cpp.hpp"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include <limits>
#include <stdint.h>


struct VirtualActionInvoker0
{
	typedef void (*Action)(void*, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		((Action)invokeData.methodPtr)(obj, invokeData.method);
	}
};
struct VirtualActionInvoker0Invoker
{
	static inline void Invoke (Il2CppMethodSlot slot, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		invokeData.method->invoker_method(il2cpp_codegen_get_method_pointer(invokeData.method), invokeData.method, obj, NULL, NULL);
	}
};
struct InterfaceActionInvoker0
{
	typedef void (*Action)(void*, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		((Action)invokeData.methodPtr)(obj, invokeData.method);
	}
};
template <typename T1>
struct InterfaceActionInvoker1Invoker;
template <typename T1>
struct InterfaceActionInvoker1Invoker<T1*>
{
	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj, T1* p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		void* params[1] = { p1 };
		invokeData.method->invoker_method(il2cpp_codegen_get_method_pointer(invokeData.method), invokeData.method, obj, params, params[0]);
	}
};
template <typename R>
struct InterfaceFuncInvoker0
{
	typedef R (*Func)(void*, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		return ((Func)invokeData.methodPtr)(obj, invokeData.method);
	}
};
template <typename R>
struct InterfaceFuncInvoker0Invoker
{
	static inline R Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		R ret;
		invokeData.method->invoker_method(il2cpp_codegen_get_method_pointer(invokeData.method), invokeData.method, obj, NULL, &ret);
		return ret;
	}
};
struct InvokerActionInvoker0
{
	static inline void Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj)
	{
		method->invoker_method(methodPtr, method, obj, NULL, NULL);
	}
};
template <typename T1>
struct InvokerActionInvoker1;
template <typename T1>
struct InvokerActionInvoker1<T1*>
{
	static inline void Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj, T1* p1)
	{
		void* params[1] = { p1 };
		method->invoker_method(methodPtr, method, obj, params, params[0]);
	}
};
template <typename T1, typename T2>
struct InvokerActionInvoker2;
template <typename T1, typename T2>
struct InvokerActionInvoker2<T1, T2*>
{
	static inline void Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj, T1 p1, T2* p2)
	{
		void* params[2] = { &p1, p2 };
		method->invoker_method(methodPtr, method, obj, params, params[1]);
	}
};
template <typename T1, typename T2>
struct InvokerActionInvoker2<T1*, T2*>
{
	static inline void Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj, T1* p1, T2* p2)
	{
		void* params[2] = { p1, p2 };
		method->invoker_method(methodPtr, method, obj, params, params[1]);
	}
};
template <typename T1, typename T2, typename T3>
struct InvokerActionInvoker3;
template <typename T1, typename T2, typename T3>
struct InvokerActionInvoker3<T1*, T2*, T3*>
{
	static inline void Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj, T1* p1, T2* p2, T3* p3)
	{
		void* params[3] = { p1, p2, p3 };
		method->invoker_method(methodPtr, method, obj, params, params[2]);
	}
};
template <typename R>
struct InvokerFuncInvoker0
{
	static inline R Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj)
	{
		R ret;
		method->invoker_method(methodPtr, method, obj, NULL, &ret);
		return ret;
	}
};
template <typename R, typename T1>
struct InvokerFuncInvoker1;
template <typename R, typename T1>
struct InvokerFuncInvoker1<R, T1*>
{
	static inline R Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj, T1* p1)
	{
		R ret;
		void* params[1] = { p1 };
		method->invoker_method(methodPtr, method, obj, params, &ret);
		return ret;
	}
};
template <typename R, typename T1, typename T2>
struct InvokerFuncInvoker2;
template <typename R, typename T1, typename T2>
struct InvokerFuncInvoker2<R, T1*, T2*>
{
	static inline R Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj, T1* p1, T2* p2)
	{
		R ret;
		void* params[2] = { p1, p2 };
		method->invoker_method(methodPtr, method, obj, params, &ret);
		return ret;
	}
};

// System.Action`1<System.Object>
struct Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87;
// System.Collections.Generic.Dictionary`2<System.Int32,System.Threading.Tasks.Task>
struct Dictionary_2_t403063CE4960B4F46C688912237C6A27E550FF55;
// System.Collections.Generic.Dictionary`2<System.String,System.Int32>
struct Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588;
// System.Func`1<System.Threading.Tasks.Task/ContingentProperties>
struct Func_1_tD59A12717D79BFB403BF973694B1BE5B85474BD1;
// System.Func`2<System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>,System.Boolean>
struct Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B;
// System.Func`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>
struct Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B;
// System.Func`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Boolean>
struct Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12;
// System.Func`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Object>
struct Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287;
// System.Func`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Single>
struct Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72;
// System.Func`2<System.Object,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>
struct Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9;
// System.Func`2<System.Object,System.Boolean>
struct Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00;
// System.Func`2<System.Object,System.Single>
struct Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12;
// System.Func`2<System.Single,System.Boolean>
struct Func_2_t49E998685259ADE759F9329BF66F20DE8667006E;
// System.Func`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,System.Boolean>
struct Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B;
// System.Func`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>
struct Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0;
// System.Collections.Generic.IEnumerable`1<System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>
struct IEnumerable_1_tE6114F182209A8965B5512E8A416145078311D8A;
// System.Collections.Generic.IEnumerable`1<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>>
struct IEnumerable_1_t15BD967A8EBA309A9C30980C26FF7704D05D6DCF;
// System.Collections.Generic.IEnumerable`1<System.Runtime.ExceptionServices.ExceptionDispatchInfo>
struct IEnumerable_1_t54BAC54AB2ADE190B60BF5A2A72312AD1BFBA7D5;
// System.Collections.Generic.IEnumerable`1<System.Object>
struct IEnumerable_1_tF95C9E01A913DD50575531C8305932628663D9E9;
// System.Collections.Generic.IEnumerable`1<System.Single>
struct IEnumerable_1_t352FDDEA001ABE8E1D67849D2E2F3D1D75B03D41;
// System.Collections.Generic.IEnumerable`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>
struct IEnumerable_1_t29E7244AE33B71FA0981E50D5BC73B7938F35C66;
// System.Collections.Generic.IEnumerator`1<System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>
struct IEnumerator_1_t98C005107BF2CBE3C3427D612381C302B5239D57;
// System.Collections.Generic.IEnumerator`1<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>>
struct IEnumerator_1_t41D19A40D91CD62855127CBCA1741D49573320D4;
// System.Collections.Generic.IEnumerator`1<System.Object>
struct IEnumerator_1_t43D2E4BA9246755F293DFA74F001FB1A70A648FD;
// System.Collections.Generic.IEnumerator`1<System.Single>
struct IEnumerator_1_t736E9F8BD2FD38A5E9EA2E8A510AFED788D05010;
// System.Collections.Generic.IEnumerator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>
struct IEnumerator_1_t75CB2681E18F7F2791528FA2CA60361FDB5DA08D;
// System.Collections.Generic.IList`1<System.Runtime.ExceptionServices.ExceptionDispatchInfo>
struct IList_1_tCFDE4BD8548909F112CB197B5B2C3F1211D52014;
// System.Linq.Enumerable/Iterator`1<System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>
struct Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40;
// System.Linq.Enumerable/Iterator`1<System.Object>
struct Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA;
// System.Linq.Enumerable/Iterator`1<System.Single>
struct Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E;
// System.Linq.Enumerable/Iterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>
struct Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0;
// System.Collections.Generic.List`1<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>>
struct List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE;
// System.Collections.Generic.List`1<System.Object>
struct List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D;
// System.Collections.Generic.List`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>
struct List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A;
// System.Collections.Generic.LowLevelListWithIList`1<System.Runtime.ExceptionServices.ExceptionDispatchInfo>
struct LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4;
// System.Collections.Generic.LowLevelListWithIList`1<System.Object>
struct LowLevelListWithIList_1_t424B84BB083921C00880052D4B49074AF66B72FC;
// System.Collections.Generic.LowLevelList`1<System.Runtime.ExceptionServices.ExceptionDispatchInfo>
struct LowLevelList_1_tD1EA453996325637EAF8C2A867D4DED46603298F;
// System.Collections.Generic.LowLevelList`1<System.Object>
struct LowLevelList_1_tFB82D019B54AD98BC47D01C80B32C9DC3FA3BE58;
// System.Predicate`1<System.Object>
struct Predicate_1_t8342C85FF4E41CD1F7024AC0CDC3E5312A32CB12;
// System.Predicate`1<System.Threading.Tasks.Task>
struct Predicate_1_t7F48518B008C1472339EEEBABA3DE203FE1F26ED;
// System.Collections.ObjectModel.ReadOnlyCollection`1<System.Runtime.ExceptionServices.ExceptionDispatchInfo>
struct ReadOnlyCollection_1_t7E3BC8E94E9BC82C2FD0D77A76BF08AC79C2CECD;
// System.Threading.Tasks.Task`1<System.Boolean[]>
struct Task_1_t69C91E4C5122CE3A171C7090781C6B14BE5D63DA;
// System.Threading.Tasks.Task`1<System.Object[]>
struct Task_1_t5B1ACE26BA285FAD19DF6E5F61190F03850816CB;
// System.Threading.Tasks.Task`1<System.Boolean>
struct Task_1_t824317F4B958F7512E8F7300511752937A6C6043;
// System.Threading.Tasks.Task`1<System.Object>
struct Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2;
// System.Threading.Tasks.Task`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>
struct Task_1_tDF1FF540D7D2248A08580387A39717B7FB7A9CF9;
// System.WeakReference`1<System.Object>
struct WeakReference_1_tED795563AD26F795CED3BBCD488AB1694E385BCE;
// System.Threading.Tasks.Task/WhenAllPromise`1<System.Boolean>
struct WhenAllPromise_1_t97739DA26ABE51AE956DAFF6EB494F6A4F190235;
// System.Threading.Tasks.Task/WhenAllPromise`1<System.Object>
struct WhenAllPromise_1_t6B7D3ABF753EA8F6F7581C787A300C50CBA54FF0;
// System.Threading.Tasks.Task/WhenAllPromise`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>
struct WhenAllPromise_1_tF47371F9AE700EE2F63999402265300771AAFDC3;
// System.Linq.Enumerable/WhereArrayIterator`1<System.Object>
struct WhereArrayIterator_1_t027D2511F9B69346688FE3E5623EF2BEE81E9FAA;
// System.Linq.Enumerable/WhereArrayIterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>
struct WhereArrayIterator_1_tA7187088CE8DF4724576F6B7F633203C144505F6;
// System.Linq.Enumerable/WhereEnumerableIterator`1<System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>
struct WhereEnumerableIterator_1_tB576B4EE76F70B81DDF11FA35260EC589825E7C5;
// System.Linq.Enumerable/WhereEnumerableIterator`1<System.Object>
struct WhereEnumerableIterator_1_t1E787D13759F5A31C94B3FAED181402B25C278F4;
// System.Linq.Enumerable/WhereEnumerableIterator`1<System.Single>
struct WhereEnumerableIterator_1_t7BB2D1D9F8A6E52243A87F45DFEEA4209D685F7C;
// System.Linq.Enumerable/WhereEnumerableIterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>
struct WhereEnumerableIterator_1_t8B24528558F527941435C4FE1D046216FE4F277B;
// System.Linq.Enumerable/WhereListIterator`1<System.Object>
struct WhereListIterator_1_t1F40F08BAF8586F2C09294085BC605CC2FA432EB;
// System.Linq.Enumerable/WhereListIterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>
struct WhereListIterator_1_tD37742ECD2F53395BA8B668C2671C4C82E8E85F0;
// System.Linq.Enumerable/WhereSelectArrayIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>
struct WhereSelectArrayIterator_2_t30000EF5692CE8B73879E35CB9EA48B4DCF6EAB5;
// System.Linq.Enumerable/WhereSelectArrayIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Object>
struct WhereSelectArrayIterator_2_tBABE519E8262C3B19F12FA57F0EBBEF62306E2EE;
// System.Linq.Enumerable/WhereSelectArrayIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Single>
struct WhereSelectArrayIterator_2_tFF39CC0F264CA7C80FEA868C00FD12A1D45DC58F;
// System.Linq.Enumerable/WhereSelectArrayIterator`2<System.Object,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>
struct WhereSelectArrayIterator_2_tB6BDCFF0C2C4DEEFA9D69A00383291FAA9B56E24;
// System.Linq.Enumerable/WhereSelectArrayIterator`2<System.Object,System.Single>
struct WhereSelectArrayIterator_2_tD87299FD8280F38D5E50B5174574F2F20188E0D4;
// System.Linq.Enumerable/WhereSelectArrayIterator`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>
struct WhereSelectArrayIterator_2_tBE026CE497BB8F36E31685722BBD7CB567570174;
// System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>
struct WhereSelectEnumerableIterator_2_t4A082C2E79F346CC9DA87508903869069C5C6454;
// System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Object>
struct WhereSelectEnumerableIterator_2_t27953926A9605639C866748EA3EF6801E63B5430;
// System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Single>
struct WhereSelectEnumerableIterator_2_tA24C2F75772C5AB98B395DC9C70CDD82327A82C4;
// System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.Object,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>
struct WhereSelectEnumerableIterator_2_t2379CEC793B1983191566FBE7853007579556057;
// System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.Object,System.Single>
struct WhereSelectEnumerableIterator_2_tF7B17AE20C591499CCC69CF4CFEA521F28D66E94;
// System.Linq.Enumerable/WhereSelectEnumerableIterator`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>
struct WhereSelectEnumerableIterator_2_t1FBA58379B31F544881FB4C45B2D102F32A71E1C;
// System.Linq.Enumerable/WhereSelectListIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>
struct WhereSelectListIterator_2_t8F14DC7F95888AA905CF6AE27A6AFC1205DC2933;
// System.Linq.Enumerable/WhereSelectListIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Object>
struct WhereSelectListIterator_2_tEB04EFC71ED85DC1E14F1FC388A8CCAF2FB25E8B;
// System.Linq.Enumerable/WhereSelectListIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Single>
struct WhereSelectListIterator_2_t87543D52D098CB5DBA7AF3A904A5A1183466C3C6;
// System.Linq.Enumerable/WhereSelectListIterator`2<System.Object,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>
struct WhereSelectListIterator_2_t6D877164A02A148F4B5F654B133D07C23FA7904A;
// System.Linq.Enumerable/WhereSelectListIterator`2<System.Object,System.Single>
struct WhereSelectListIterator_2_tFE32164179EE37B666D799CFB7F5167FB4C464D9;
// System.Linq.Enumerable/WhereSelectListIterator`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>
struct WhereSelectListIterator_2_t86EE6817E8A1706688C6D82D82C9D44BC99CC336;
// System.Threading.Tasks.Task`1<System.Boolean>[]
struct Task_1U5BU5D_tBA390D600BBF7458FC9BD634B8F6E52B5D753C91;
// System.Threading.Tasks.Task`1<System.Object>[]
struct Task_1U5BU5D_tE516FE52659CC40D166035B14BAEFD6B275FBA1E;
// System.Threading.Tasks.Task`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>[]
struct Task_1U5BU5D_t46133A6F873A9EC9FC75DAB1315ABC8025565F09;
// System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>[]
struct ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46;
// System.Boolean[]
struct BooleanU5BU5D_tD317D27C31DB892BE79FAE3AEBC0B3FFB73DE9B4;
// System.Delegate[]
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
// System.Runtime.ExceptionServices.ExceptionDispatchInfo[]
struct ExceptionDispatchInfoU5BU5D_t98D150CF7B222A428B342E1D1F7B69D64BE1A536;
// System.IntPtr[]
struct IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832;
// System.Object[]
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;
// RBPhys.RBCollider[]
struct RBColliderU5BU5D_tDE96D161B9B780342B1676DB63FF9DCC477D3B9B;
// System.Diagnostics.StackTrace[]
struct StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF;
// System.String[]
struct StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248;
// System.Threading.Tasks.Task[]
struct TaskU5BU5D_t368E447BD9A179BA9A26BAAABF1BAE9CA79F60B3;
// System.Type[]
struct TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB;
// Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType[]
struct __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC;
// System.ArgumentNullException
struct ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129;
// System.Reflection.Binder
struct Binder_t91BFCE95A7057FADF4D8A1A342AFE52872246235;
// System.Threading.CancellationTokenSource
struct CancellationTokenSource_tAAE1E0033BCFC233801F8CB4CED5C852B350CB7B;
// System.Threading.ContextCallback
struct ContextCallback_tE8AFBDBFCC040FDA8DA8C1EEFE9BD66B16BDA007;
// System.Delegate
struct Delegate_t;
// System.DelegateData
struct DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E;
// System.Exception
struct Exception_t;
// System.Runtime.ExceptionServices.ExceptionDispatchInfo
struct ExceptionDispatchInfo_tD7AF19E75FEC22F4A8329FD1E9EDF96615CB2757;
// System.Collections.IDictionary
struct IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220;
// System.Runtime.Serialization.IFormatterConverter
struct IFormatterConverter_t726606DAC82C384B08C82471313C340968DDB609;
// System.Threading.Tasks.ITaskCompletionAction
struct ITaskCompletionAction_t8023B55CEBE5EFBC7531E61152CF41351A0C9388;
// System.Reflection.MemberFilter
struct MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553;
// System.Reflection.MethodInfo
struct MethodInfo_t;
// RBPhys.RBCollider
struct RBCollider_t23F94AE4EF06C21499A0CED32EADD3F4A6D36E6C;
// RBPhys.RBRigidbody
struct RBRigidbody_t76882C0EBFE95456DDB0C0358162A84DB06239AD;
// System.Runtime.Serialization.SafeSerializationManager
struct SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6;
// System.Runtime.Serialization.SerializationInfo
struct SerializationInfo_t3C47F63E24BEB9FCE2DC6309E027F238DC5C5E37;
// System.Threading.Tasks.StackGuard
struct StackGuard_tACE063A1B7374BDF4AD472DE4585D05AD8745352;
// System.String
struct String_t;
// System.Threading.Tasks.Task
struct Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572;
// System.Threading.Tasks.TaskFactory
struct TaskFactory_tF781BD37BE23917412AD83424D1497C7C1509DF0;
// System.Threading.Tasks.TaskScheduler
struct TaskScheduler_t3F0550EBEF7C41F74EC8C08FF4BED0D8CE66006E;
// System.Type
struct Type_t;
// System.Void
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;
// System.Threading.Tasks.Task/ContingentProperties
struct ContingentProperties_t3FA59480914505CEA917B1002EC675F29D0CB540;

IL2CPP_EXTERN_C RuntimeClass* ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Type_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C String_t* _stringLiteral5CA6E7C0AE72196B2817D93A78C719652EC691C0;
IL2CPP_EXTERN_C String_t* _stringLiteral7D20B8219CA0491872B2E811B262066A5DD875A7;
IL2CPP_EXTERN_C String_t* _stringLiteralA7B00F7F25C375B2501A6ADBC86D092B23977085;
IL2CPP_EXTERN_C String_t* _stringLiteralD68A68585C704F4E081A45928451F951EF468E68;
IL2CPP_EXTERN_C const RuntimeMethod* LowLevelListWithIList_1__ctor_m0E3A007F2F2CA220DB5C20F85BD60FE2F4A045E2_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* LowLevelList_1_AddRange_mD5D6366F78D1A613E03AE6A45654DE4C2A6C51BE_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* WeakReference_1_GetObjectData_m6F2E12AF126FAE536995F52F9501498BDA5917A7_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* WeakReference_1__ctor_m2289DC7F3597E1BA77555086A86F91807FDC96E2_RuntimeMethod_var;
struct Delegate_t_marshaled_com;
struct Delegate_t_marshaled_pinvoke;
struct Exception_t_marshaled_com;
struct Exception_t_marshaled_pinvoke;

struct Task_1U5BU5D_tBA390D600BBF7458FC9BD634B8F6E52B5D753C91;
struct Task_1U5BU5D_tE516FE52659CC40D166035B14BAEFD6B275FBA1E;
struct Task_1U5BU5D_t46133A6F873A9EC9FC75DAB1315ABC8025565F09;
struct ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46;
struct BooleanU5BU5D_tD317D27C31DB892BE79FAE3AEBC0B3FFB73DE9B4;
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;
struct TaskU5BU5D_t368E447BD9A179BA9A26BAAABF1BAE9CA79F60B3;
struct __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// System.Linq.Enumerable/Iterator`1<System.Object>
struct Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA  : public RuntimeObject
{
	// System.Int32 System.Linq.Enumerable/Iterator`1::threadId
	int32_t ___threadId_0;
	// System.Int32 System.Linq.Enumerable/Iterator`1::state
	int32_t ___state_1;
	// TSource System.Linq.Enumerable/Iterator`1::current
	RuntimeObject* ___current_2;
};

// System.Linq.Enumerable/Iterator`1<System.Single>
struct Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E  : public RuntimeObject
{
	// System.Int32 System.Linq.Enumerable/Iterator`1::threadId
	int32_t ___threadId_0;
	// System.Int32 System.Linq.Enumerable/Iterator`1::state
	int32_t ___state_1;
	// TSource System.Linq.Enumerable/Iterator`1::current
	float ___current_2;
};

// System.Linq.Enumerable/Iterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>
struct Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0 : public RuntimeObject {};

// System.Collections.Generic.List`1<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>>
struct List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE  : public RuntimeObject
{
	// T[] System.Collections.Generic.List`1::_items
	ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46* ____items_1;
	// System.Int32 System.Collections.Generic.List`1::_size
	int32_t ____size_2;
	// System.Int32 System.Collections.Generic.List`1::_version
	int32_t ____version_3;
	// System.Object System.Collections.Generic.List`1::_syncRoot
	RuntimeObject* ____syncRoot_4;
};

struct List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE_StaticFields
{
	// T[] System.Collections.Generic.List`1::s_emptyArray
	ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46* ___s_emptyArray_5;
};

// System.Collections.Generic.List`1<System.Object>
struct List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D  : public RuntimeObject
{
	// T[] System.Collections.Generic.List`1::_items
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ____items_1;
	// System.Int32 System.Collections.Generic.List`1::_size
	int32_t ____size_2;
	// System.Int32 System.Collections.Generic.List`1::_version
	int32_t ____version_3;
	// System.Object System.Collections.Generic.List`1::_syncRoot
	RuntimeObject* ____syncRoot_4;
};

struct List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D_StaticFields
{
	// T[] System.Collections.Generic.List`1::s_emptyArray
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___s_emptyArray_5;
};

// System.Collections.Generic.List`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>
struct List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A  : public RuntimeObject
{
	// T[] System.Collections.Generic.List`1::_items
	__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* ____items_1;
	// System.Int32 System.Collections.Generic.List`1::_size
	int32_t ____size_2;
	// System.Int32 System.Collections.Generic.List`1::_version
	int32_t ____version_3;
	// System.Object System.Collections.Generic.List`1::_syncRoot
	RuntimeObject* ____syncRoot_4;
};

struct List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A_StaticFields
{
	// T[] System.Collections.Generic.List`1::s_emptyArray
	__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* ___s_emptyArray_5;
};

// System.Collections.Generic.LowLevelList`1<System.Runtime.ExceptionServices.ExceptionDispatchInfo>
struct LowLevelList_1_tD1EA453996325637EAF8C2A867D4DED46603298F  : public RuntimeObject
{
	// T[] System.Collections.Generic.LowLevelList`1::_items
	ExceptionDispatchInfoU5BU5D_t98D150CF7B222A428B342E1D1F7B69D64BE1A536* ____items_0;
	// System.Int32 System.Collections.Generic.LowLevelList`1::_size
	int32_t ____size_1;
	// System.Int32 System.Collections.Generic.LowLevelList`1::_version
	int32_t ____version_2;
};

struct LowLevelList_1_tD1EA453996325637EAF8C2A867D4DED46603298F_StaticFields
{
	// T[] System.Collections.Generic.LowLevelList`1::s_emptyArray
	ExceptionDispatchInfoU5BU5D_t98D150CF7B222A428B342E1D1F7B69D64BE1A536* ___s_emptyArray_3;
};

// System.Collections.ObjectModel.ReadOnlyCollection`1<System.Runtime.ExceptionServices.ExceptionDispatchInfo>
struct ReadOnlyCollection_1_t7E3BC8E94E9BC82C2FD0D77A76BF08AC79C2CECD  : public RuntimeObject
{
	// System.Collections.Generic.IList`1<T> System.Collections.ObjectModel.ReadOnlyCollection`1::list
	RuntimeObject* ___list_0;
	// System.Object System.Collections.ObjectModel.ReadOnlyCollection`1::_syncRoot
	RuntimeObject* ____syncRoot_1;
};
struct Il2CppArrayBounds;

// System.Runtime.ExceptionServices.ExceptionDispatchInfo
struct ExceptionDispatchInfo_tD7AF19E75FEC22F4A8329FD1E9EDF96615CB2757  : public RuntimeObject
{
	// System.Exception System.Runtime.ExceptionServices.ExceptionDispatchInfo::m_Exception
	Exception_t* ___m_Exception_0;
	// System.Object System.Runtime.ExceptionServices.ExceptionDispatchInfo::m_stackTrace
	RuntimeObject* ___m_stackTrace_1;
};

// System.Reflection.MemberInfo
struct MemberInfo_t  : public RuntimeObject
{
};

// System.Runtime.Serialization.SerializationInfo
struct SerializationInfo_t3C47F63E24BEB9FCE2DC6309E027F238DC5C5E37  : public RuntimeObject
{
	// System.String[] System.Runtime.Serialization.SerializationInfo::m_members
	StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* ___m_members_0;
	// System.Object[] System.Runtime.Serialization.SerializationInfo::m_data
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___m_data_1;
	// System.Type[] System.Runtime.Serialization.SerializationInfo::m_types
	TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB* ___m_types_2;
	// System.Collections.Generic.Dictionary`2<System.String,System.Int32> System.Runtime.Serialization.SerializationInfo::m_nameToIndex
	Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588* ___m_nameToIndex_3;
	// System.Int32 System.Runtime.Serialization.SerializationInfo::m_currMember
	int32_t ___m_currMember_4;
	// System.Runtime.Serialization.IFormatterConverter System.Runtime.Serialization.SerializationInfo::m_converter
	RuntimeObject* ___m_converter_5;
	// System.String System.Runtime.Serialization.SerializationInfo::m_fullTypeName
	String_t* ___m_fullTypeName_6;
	// System.String System.Runtime.Serialization.SerializationInfo::m_assemName
	String_t* ___m_assemName_7;
	// System.Type System.Runtime.Serialization.SerializationInfo::objectType
	Type_t* ___objectType_8;
	// System.Boolean System.Runtime.Serialization.SerializationInfo::isFullTypeNameSetExplicit
	bool ___isFullTypeNameSetExplicit_9;
	// System.Boolean System.Runtime.Serialization.SerializationInfo::isAssemblyNameSetExplicit
	bool ___isAssemblyNameSetExplicit_10;
	// System.Boolean System.Runtime.Serialization.SerializationInfo::requireSameTokenInPartialTrust
	bool ___requireSameTokenInPartialTrust_11;
};

// System.String
struct String_t  : public RuntimeObject
{
	// System.Int32 System.String::_stringLength
	int32_t ____stringLength_4;
	// System.Char System.String::_firstChar
	Il2CppChar ____firstChar_5;
};

struct String_t_StaticFields
{
	// System.String System.String::Empty
	String_t* ___Empty_6;
};

// System.Threading.Tasks.Task
struct Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572  : public RuntimeObject
{
	// System.Int32 modreq(System.Runtime.CompilerServices.IsVolatile) System.Threading.Tasks.Task::m_taskId
	int32_t ___m_taskId_1;
	// System.Delegate System.Threading.Tasks.Task::m_action
	Delegate_t* ___m_action_2;
	// System.Object System.Threading.Tasks.Task::m_stateObject
	RuntimeObject* ___m_stateObject_3;
	// System.Threading.Tasks.TaskScheduler System.Threading.Tasks.Task::m_taskScheduler
	TaskScheduler_t3F0550EBEF7C41F74EC8C08FF4BED0D8CE66006E* ___m_taskScheduler_4;
	// System.Threading.Tasks.Task System.Threading.Tasks.Task::m_parent
	Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* ___m_parent_5;
	// System.Int32 modreq(System.Runtime.CompilerServices.IsVolatile) System.Threading.Tasks.Task::m_stateFlags
	int32_t ___m_stateFlags_6;
	// System.Object modreq(System.Runtime.CompilerServices.IsVolatile) System.Threading.Tasks.Task::m_continuationObject
	RuntimeObject* ___m_continuationObject_7;
	// System.Threading.Tasks.Task/ContingentProperties modreq(System.Runtime.CompilerServices.IsVolatile) System.Threading.Tasks.Task::m_contingentProperties
	ContingentProperties_t3FA59480914505CEA917B1002EC675F29D0CB540* ___m_contingentProperties_10;
};

struct Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572_StaticFields
{
	// System.Int32 System.Threading.Tasks.Task::s_taskIdCounter
	int32_t ___s_taskIdCounter_0;
	// System.Object System.Threading.Tasks.Task::s_taskCompletionSentinel
	RuntimeObject* ___s_taskCompletionSentinel_8;
	// System.Boolean System.Threading.Tasks.Task::s_asyncDebuggingEnabled
	bool ___s_asyncDebuggingEnabled_9;
	// System.Action`1<System.Object> System.Threading.Tasks.Task::s_taskCancelCallback
	Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87* ___s_taskCancelCallback_11;
	// System.Func`1<System.Threading.Tasks.Task/ContingentProperties> System.Threading.Tasks.Task::s_createContingentProperties
	Func_1_tD59A12717D79BFB403BF973694B1BE5B85474BD1* ___s_createContingentProperties_14;
	// System.Threading.Tasks.TaskFactory System.Threading.Tasks.Task::<Factory>k__BackingField
	TaskFactory_tF781BD37BE23917412AD83424D1497C7C1509DF0* ___U3CFactoryU3Ek__BackingField_15;
	// System.Threading.Tasks.Task System.Threading.Tasks.Task::<CompletedTask>k__BackingField
	Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* ___U3CCompletedTaskU3Ek__BackingField_16;
	// System.Predicate`1<System.Threading.Tasks.Task> System.Threading.Tasks.Task::s_IsExceptionObservedByParentPredicate
	Predicate_1_t7F48518B008C1472339EEEBABA3DE203FE1F26ED* ___s_IsExceptionObservedByParentPredicate_17;
	// System.Threading.ContextCallback System.Threading.Tasks.Task::s_ecCallback
	ContextCallback_tE8AFBDBFCC040FDA8DA8C1EEFE9BD66B16BDA007* ___s_ecCallback_18;
	// System.Predicate`1<System.Object> System.Threading.Tasks.Task::s_IsTaskContinuationNullPredicate
	Predicate_1_t8342C85FF4E41CD1F7024AC0CDC3E5312A32CB12* ___s_IsTaskContinuationNullPredicate_19;
	// System.Collections.Generic.Dictionary`2<System.Int32,System.Threading.Tasks.Task> System.Threading.Tasks.Task::s_currentActiveTasks
	Dictionary_2_t403063CE4960B4F46C688912237C6A27E550FF55* ___s_currentActiveTasks_20;
	// System.Object System.Threading.Tasks.Task::s_activeTasksLock
	RuntimeObject* ___s_activeTasksLock_21;
};

struct Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572_ThreadStaticFields
{
	// System.Threading.Tasks.Task System.Threading.Tasks.Task::t_currentTask
	Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* ___t_currentTask_12;
	// System.Threading.Tasks.StackGuard System.Threading.Tasks.Task::t_stackGuard
	StackGuard_tACE063A1B7374BDF4AD472DE4585D05AD8745352* ___t_stackGuard_13;
};

// System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F  : public RuntimeObject
{
};
// Native definition for P/Invoke marshalling of System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_pinvoke
{
};
// Native definition for COM marshalling of System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_com
{
};

// System.Collections.Generic.List`1/Enumerator<System.Object>
struct Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A 
{
	// System.Collections.Generic.List`1<T> System.Collections.Generic.List`1/Enumerator::_list
	List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* ____list_0;
	// System.Int32 System.Collections.Generic.List`1/Enumerator::_index
	int32_t ____index_1;
	// System.Int32 System.Collections.Generic.List`1/Enumerator::_version
	int32_t ____version_2;
	// T System.Collections.Generic.List`1/Enumerator::_current
	RuntimeObject* ____current_3;
};

// System.Collections.Generic.List`1/Enumerator<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>
typedef Il2CppFullySharedGenericStruct Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF;

// System.Collections.Generic.LowLevelListWithIList`1<System.Runtime.ExceptionServices.ExceptionDispatchInfo>
struct LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4  : public LowLevelList_1_tD1EA453996325637EAF8C2A867D4DED46603298F
{
};

// System.Threading.Tasks.Task`1<System.Boolean[]>
struct Task_1_t69C91E4C5122CE3A171C7090781C6B14BE5D63DA  : public Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572
{
	// TResult System.Threading.Tasks.Task`1::m_result
	BooleanU5BU5D_tD317D27C31DB892BE79FAE3AEBC0B3FFB73DE9B4* ___m_result_22;
};

// System.Threading.Tasks.Task`1<System.Object[]>
struct Task_1_t5B1ACE26BA285FAD19DF6E5F61190F03850816CB  : public Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572
{
	// TResult System.Threading.Tasks.Task`1::m_result
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___m_result_22;
};

// System.Threading.Tasks.Task`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType[]>
struct Task_1_tEA323ABE4B135A0CC68B2D7C3498CC3DA6B8D802  : public Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572
{
	// TResult System.Threading.Tasks.Task`1::m_result
	__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* ___m_result_22;
};

// System.Threading.Tasks.Task`1<System.Boolean>
struct Task_1_t824317F4B958F7512E8F7300511752937A6C6043  : public Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572
{
	// TResult System.Threading.Tasks.Task`1::m_result
	bool ___m_result_22;
};

// System.Threading.Tasks.Task`1<System.Object>
struct Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2  : public Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572
{
	// TResult System.Threading.Tasks.Task`1::m_result
	RuntimeObject* ___m_result_22;
};

// System.Threading.Tasks.Task`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>
struct Task_1_tDF1FF540D7D2248A08580387A39717B7FB7A9CF9 : public Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572 {};

// System.Linq.Enumerable/WhereArrayIterator`1<System.Object>
struct WhereArrayIterator_1_t027D2511F9B69346688FE3E5623EF2BEE81E9FAA  : public Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA
{
	// TSource[] System.Linq.Enumerable/WhereArrayIterator`1::source
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___source_3;
	// System.Func`2<TSource,System.Boolean> System.Linq.Enumerable/WhereArrayIterator`1::predicate
	Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate_4;
	// System.Int32 System.Linq.Enumerable/WhereArrayIterator`1::index
	int32_t ___index_5;
};

// System.Linq.Enumerable/WhereArrayIterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>
struct WhereArrayIterator_1_tA7187088CE8DF4724576F6B7F633203C144505F6 : public RuntimeObject {};

// System.Linq.Enumerable/WhereEnumerableIterator`1<System.Object>
struct WhereEnumerableIterator_1_t1E787D13759F5A31C94B3FAED181402B25C278F4  : public Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA
{
	// System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable/WhereEnumerableIterator`1::source
	RuntimeObject* ___source_3;
	// System.Func`2<TSource,System.Boolean> System.Linq.Enumerable/WhereEnumerableIterator`1::predicate
	Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate_4;
	// System.Collections.Generic.IEnumerator`1<TSource> System.Linq.Enumerable/WhereEnumerableIterator`1::enumerator
	RuntimeObject* ___enumerator_5;
};

// System.Linq.Enumerable/WhereEnumerableIterator`1<System.Single>
struct WhereEnumerableIterator_1_t7BB2D1D9F8A6E52243A87F45DFEEA4209D685F7C  : public Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E
{
	// System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable/WhereEnumerableIterator`1::source
	RuntimeObject* ___source_3;
	// System.Func`2<TSource,System.Boolean> System.Linq.Enumerable/WhereEnumerableIterator`1::predicate
	Func_2_t49E998685259ADE759F9329BF66F20DE8667006E* ___predicate_4;
	// System.Collections.Generic.IEnumerator`1<TSource> System.Linq.Enumerable/WhereEnumerableIterator`1::enumerator
	RuntimeObject* ___enumerator_5;
};

// System.Linq.Enumerable/WhereEnumerableIterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>
struct WhereEnumerableIterator_1_t8B24528558F527941435C4FE1D046216FE4F277B : public RuntimeObject {};

// System.Linq.Enumerable/WhereSelectArrayIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Object>
struct WhereSelectArrayIterator_2_tBABE519E8262C3B19F12FA57F0EBBEF62306E2EE  : public Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA
{
	// TSource[] System.Linq.Enumerable/WhereSelectArrayIterator`2::source
	ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46* ___source_3;
	// System.Func`2<TSource,System.Boolean> System.Linq.Enumerable/WhereSelectArrayIterator`2::predicate
	Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate_4;
	// System.Func`2<TSource,TResult> System.Linq.Enumerable/WhereSelectArrayIterator`2::selector
	Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287* ___selector_5;
	// System.Int32 System.Linq.Enumerable/WhereSelectArrayIterator`2::index
	int32_t ___index_6;
};

// System.Linq.Enumerable/WhereSelectArrayIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Single>
struct WhereSelectArrayIterator_2_tFF39CC0F264CA7C80FEA868C00FD12A1D45DC58F  : public Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E
{
	// TSource[] System.Linq.Enumerable/WhereSelectArrayIterator`2::source
	ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46* ___source_3;
	// System.Func`2<TSource,System.Boolean> System.Linq.Enumerable/WhereSelectArrayIterator`2::predicate
	Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate_4;
	// System.Func`2<TSource,TResult> System.Linq.Enumerable/WhereSelectArrayIterator`2::selector
	Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72* ___selector_5;
	// System.Int32 System.Linq.Enumerable/WhereSelectArrayIterator`2::index
	int32_t ___index_6;
};

// System.Linq.Enumerable/WhereSelectArrayIterator`2<System.Object,System.Single>
struct WhereSelectArrayIterator_2_tD87299FD8280F38D5E50B5174574F2F20188E0D4  : public Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E
{
	// TSource[] System.Linq.Enumerable/WhereSelectArrayIterator`2::source
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___source_3;
	// System.Func`2<TSource,System.Boolean> System.Linq.Enumerable/WhereSelectArrayIterator`2::predicate
	Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate_4;
	// System.Func`2<TSource,TResult> System.Linq.Enumerable/WhereSelectArrayIterator`2::selector
	Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12* ___selector_5;
	// System.Int32 System.Linq.Enumerable/WhereSelectArrayIterator`2::index
	int32_t ___index_6;
};

// System.Linq.Enumerable/WhereSelectArrayIterator`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>
struct WhereSelectArrayIterator_2_tBE026CE497BB8F36E31685722BBD7CB567570174 : public RuntimeObject {};

// System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Object>
struct WhereSelectEnumerableIterator_2_t27953926A9605639C866748EA3EF6801E63B5430  : public Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA
{
	// System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable/WhereSelectEnumerableIterator`2::source
	RuntimeObject* ___source_3;
	// System.Func`2<TSource,System.Boolean> System.Linq.Enumerable/WhereSelectEnumerableIterator`2::predicate
	Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate_4;
	// System.Func`2<TSource,TResult> System.Linq.Enumerable/WhereSelectEnumerableIterator`2::selector
	Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287* ___selector_5;
	// System.Collections.Generic.IEnumerator`1<TSource> System.Linq.Enumerable/WhereSelectEnumerableIterator`2::enumerator
	RuntimeObject* ___enumerator_6;
};

// System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Single>
struct WhereSelectEnumerableIterator_2_tA24C2F75772C5AB98B395DC9C70CDD82327A82C4  : public Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E
{
	// System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable/WhereSelectEnumerableIterator`2::source
	RuntimeObject* ___source_3;
	// System.Func`2<TSource,System.Boolean> System.Linq.Enumerable/WhereSelectEnumerableIterator`2::predicate
	Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate_4;
	// System.Func`2<TSource,TResult> System.Linq.Enumerable/WhereSelectEnumerableIterator`2::selector
	Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72* ___selector_5;
	// System.Collections.Generic.IEnumerator`1<TSource> System.Linq.Enumerable/WhereSelectEnumerableIterator`2::enumerator
	RuntimeObject* ___enumerator_6;
};

// System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.Object,System.Single>
struct WhereSelectEnumerableIterator_2_tF7B17AE20C591499CCC69CF4CFEA521F28D66E94  : public Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E
{
	// System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable/WhereSelectEnumerableIterator`2::source
	RuntimeObject* ___source_3;
	// System.Func`2<TSource,System.Boolean> System.Linq.Enumerable/WhereSelectEnumerableIterator`2::predicate
	Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate_4;
	// System.Func`2<TSource,TResult> System.Linq.Enumerable/WhereSelectEnumerableIterator`2::selector
	Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12* ___selector_5;
	// System.Collections.Generic.IEnumerator`1<TSource> System.Linq.Enumerable/WhereSelectEnumerableIterator`2::enumerator
	RuntimeObject* ___enumerator_6;
};

// System.Linq.Enumerable/WhereSelectEnumerableIterator`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>
struct WhereSelectEnumerableIterator_2_t1FBA58379B31F544881FB4C45B2D102F32A71E1C : public RuntimeObject {};

// System.Boolean
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	// System.Boolean System.Boolean::m_value
	bool ___m_value_0;
};

struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	// System.String System.Boolean::TrueString
	String_t* ___TrueString_5;
	// System.String System.Boolean::FalseString
	String_t* ___FalseString_6;
};

// System.Threading.CancellationToken
struct CancellationToken_t51142D9C6D7C02D314DA34A6A7988C528992FFED 
{
	// System.Threading.CancellationTokenSource System.Threading.CancellationToken::_source
	CancellationTokenSource_tAAE1E0033BCFC233801F8CB4CED5C852B350CB7B* ____source_0;
};

struct CancellationToken_t51142D9C6D7C02D314DA34A6A7988C528992FFED_StaticFields
{
	// System.Action`1<System.Object> System.Threading.CancellationToken::s_actionToActionObjShunt
	Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87* ___s_actionToActionObjShunt_1;
};
// Native definition for P/Invoke marshalling of System.Threading.CancellationToken
struct CancellationToken_t51142D9C6D7C02D314DA34A6A7988C528992FFED_marshaled_pinvoke
{
	CancellationTokenSource_tAAE1E0033BCFC233801F8CB4CED5C852B350CB7B* ____source_0;
};
// Native definition for COM marshalling of System.Threading.CancellationToken
struct CancellationToken_t51142D9C6D7C02D314DA34A6A7988C528992FFED_marshaled_com
{
	CancellationTokenSource_tAAE1E0033BCFC233801F8CB4CED5C852B350CB7B* ____source_0;
};

// System.Int32
struct Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C 
{
	// System.Int32 System.Int32::m_value
	int32_t ___m_value_0;
};

// System.IntPtr
struct IntPtr_t 
{
	// System.Void* System.IntPtr::m_value
	void* ___m_value_0;
};

struct IntPtr_t_StaticFields
{
	// System.IntPtr System.IntPtr::Zero
	intptr_t ___Zero_1;
};

// System.Single
struct Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C 
{
	// System.Single System.Single::m_value
	float ___m_value_0;
};

// System.Runtime.Serialization.StreamingContext
struct StreamingContext_t56760522A751890146EE45F82F866B55B7E33677 
{
	// System.Object System.Runtime.Serialization.StreamingContext::m_additionalContext
	RuntimeObject* ___m_additionalContext_0;
	// System.Runtime.Serialization.StreamingContextStates System.Runtime.Serialization.StreamingContext::m_state
	int32_t ___m_state_1;
};
// Native definition for P/Invoke marshalling of System.Runtime.Serialization.StreamingContext
struct StreamingContext_t56760522A751890146EE45F82F866B55B7E33677_marshaled_pinvoke
{
	Il2CppIUnknown* ___m_additionalContext_0;
	int32_t ___m_state_1;
};
// Native definition for COM marshalling of System.Runtime.Serialization.StreamingContext
struct StreamingContext_t56760522A751890146EE45F82F866B55B7E33677_marshaled_com
{
	Il2CppIUnknown* ___m_additionalContext_0;
	int32_t ___m_state_1;
};

// System.UInt64
struct UInt64_t8F12534CC8FC4B5860F2A2CD1EE79D322E7A41AF 
{
	// System.UInt64 System.UInt64::m_value
	uint64_t ___m_value_0;
};

// UnityEngine.Vector3
struct Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 
{
	// System.Single UnityEngine.Vector3::x
	float ___x_2;
	// System.Single UnityEngine.Vector3::y
	float ___y_3;
	// System.Single UnityEngine.Vector3::z
	float ___z_4;
};

struct Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_StaticFields
{
	// UnityEngine.Vector3 UnityEngine.Vector3::zeroVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___zeroVector_5;
	// UnityEngine.Vector3 UnityEngine.Vector3::oneVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___oneVector_6;
	// UnityEngine.Vector3 UnityEngine.Vector3::upVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___upVector_7;
	// UnityEngine.Vector3 UnityEngine.Vector3::downVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___downVector_8;
	// UnityEngine.Vector3 UnityEngine.Vector3::leftVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___leftVector_9;
	// UnityEngine.Vector3 UnityEngine.Vector3::rightVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___rightVector_10;
	// UnityEngine.Vector3 UnityEngine.Vector3::forwardVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___forwardVector_11;
	// UnityEngine.Vector3 UnityEngine.Vector3::backVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___backVector_12;
	// UnityEngine.Vector3 UnityEngine.Vector3::positiveInfinityVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___positiveInfinityVector_13;
	// UnityEngine.Vector3 UnityEngine.Vector3::negativeInfinityVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___negativeInfinityVector_14;
};

// System.Void
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915 
{
	union
	{
		struct
		{
		};
		uint8_t Void_t4861ACF8F4594C3437BB48B6E56783494B843915__padding[1];
	};
};

// System.Threading.Tasks.Task/WhenAllPromise`1<System.Boolean>
struct WhenAllPromise_1_t97739DA26ABE51AE956DAFF6EB494F6A4F190235  : public Task_1_t69C91E4C5122CE3A171C7090781C6B14BE5D63DA
{
	// System.Threading.Tasks.Task`1<T>[] System.Threading.Tasks.Task/WhenAllPromise`1::m_tasks
	Task_1U5BU5D_tBA390D600BBF7458FC9BD634B8F6E52B5D753C91* ___m_tasks_23;
	// System.Int32 System.Threading.Tasks.Task/WhenAllPromise`1::m_count
	int32_t ___m_count_24;
};

// System.Threading.Tasks.Task/WhenAllPromise`1<System.Object>
struct WhenAllPromise_1_t6B7D3ABF753EA8F6F7581C787A300C50CBA54FF0  : public Task_1_t5B1ACE26BA285FAD19DF6E5F61190F03850816CB
{
	// System.Threading.Tasks.Task`1<T>[] System.Threading.Tasks.Task/WhenAllPromise`1::m_tasks
	Task_1U5BU5D_tE516FE52659CC40D166035B14BAEFD6B275FBA1E* ___m_tasks_23;
	// System.Int32 System.Threading.Tasks.Task/WhenAllPromise`1::m_count
	int32_t ___m_count_24;
};

// System.Threading.Tasks.Task/WhenAllPromise`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>
struct WhenAllPromise_1_tF47371F9AE700EE2F63999402265300771AAFDC3  : public Task_1_tEA323ABE4B135A0CC68B2D7C3498CC3DA6B8D802
{
	// System.Threading.Tasks.Task`1<T>[] System.Threading.Tasks.Task/WhenAllPromise`1::m_tasks
	Task_1U5BU5D_t46133A6F873A9EC9FC75DAB1315ABC8025565F09* ___m_tasks_23;
	// System.Int32 System.Threading.Tasks.Task/WhenAllPromise`1::m_count
	int32_t ___m_count_24;
};

// System.Linq.Enumerable/WhereListIterator`1<System.Object>
struct WhereListIterator_1_t1F40F08BAF8586F2C09294085BC605CC2FA432EB  : public Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA
{
	// System.Collections.Generic.List`1<TSource> System.Linq.Enumerable/WhereListIterator`1::source
	List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* ___source_3;
	// System.Func`2<TSource,System.Boolean> System.Linq.Enumerable/WhereListIterator`1::predicate
	Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate_4;
	// System.Collections.Generic.List`1/Enumerator<TSource> System.Linq.Enumerable/WhereListIterator`1::enumerator
	Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A ___enumerator_5;
};

// System.Linq.Enumerable/WhereListIterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>
struct WhereListIterator_1_tD37742ECD2F53395BA8B668C2671C4C82E8E85F0 : public RuntimeObject {};

// System.Linq.Enumerable/WhereSelectListIterator`2<System.Object,System.Single>
struct WhereSelectListIterator_2_tFE32164179EE37B666D799CFB7F5167FB4C464D9  : public Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E
{
	// System.Collections.Generic.List`1<TSource> System.Linq.Enumerable/WhereSelectListIterator`2::source
	List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* ___source_3;
	// System.Func`2<TSource,System.Boolean> System.Linq.Enumerable/WhereSelectListIterator`2::predicate
	Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate_4;
	// System.Func`2<TSource,TResult> System.Linq.Enumerable/WhereSelectListIterator`2::selector
	Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12* ___selector_5;
	// System.Collections.Generic.List`1/Enumerator<TSource> System.Linq.Enumerable/WhereSelectListIterator`2::enumerator
	Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A ___enumerator_6;
};

// System.Linq.Enumerable/WhereSelectListIterator`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>
struct WhereSelectListIterator_2_t86EE6817E8A1706688C6D82D82C9D44BC99CC336 : public RuntimeObject {};

// System.Delegate
struct Delegate_t  : public RuntimeObject
{
	// System.IntPtr System.Delegate::method_ptr
	Il2CppMethodPointer ___method_ptr_0;
	// System.IntPtr System.Delegate::invoke_impl
	intptr_t ___invoke_impl_1;
	// System.Object System.Delegate::m_target
	RuntimeObject* ___m_target_2;
	// System.IntPtr System.Delegate::method
	intptr_t ___method_3;
	// System.IntPtr System.Delegate::delegate_trampoline
	intptr_t ___delegate_trampoline_4;
	// System.IntPtr System.Delegate::extra_arg
	intptr_t ___extra_arg_5;
	// System.IntPtr System.Delegate::method_code
	intptr_t ___method_code_6;
	// System.IntPtr System.Delegate::interp_method
	intptr_t ___interp_method_7;
	// System.IntPtr System.Delegate::interp_invoke_impl
	intptr_t ___interp_invoke_impl_8;
	// System.Reflection.MethodInfo System.Delegate::method_info
	MethodInfo_t* ___method_info_9;
	// System.Reflection.MethodInfo System.Delegate::original_method_info
	MethodInfo_t* ___original_method_info_10;
	// System.DelegateData System.Delegate::data
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data_11;
	// System.Boolean System.Delegate::method_is_virtual
	bool ___method_is_virtual_12;
};
// Native definition for P/Invoke marshalling of System.Delegate
struct Delegate_t_marshaled_pinvoke
{
	intptr_t ___method_ptr_0;
	intptr_t ___invoke_impl_1;
	Il2CppIUnknown* ___m_target_2;
	intptr_t ___method_3;
	intptr_t ___delegate_trampoline_4;
	intptr_t ___extra_arg_5;
	intptr_t ___method_code_6;
	intptr_t ___interp_method_7;
	intptr_t ___interp_invoke_impl_8;
	MethodInfo_t* ___method_info_9;
	MethodInfo_t* ___original_method_info_10;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data_11;
	int32_t ___method_is_virtual_12;
};
// Native definition for COM marshalling of System.Delegate
struct Delegate_t_marshaled_com
{
	intptr_t ___method_ptr_0;
	intptr_t ___invoke_impl_1;
	Il2CppIUnknown* ___m_target_2;
	intptr_t ___method_3;
	intptr_t ___delegate_trampoline_4;
	intptr_t ___extra_arg_5;
	intptr_t ___method_code_6;
	intptr_t ___interp_method_7;
	intptr_t ___interp_invoke_impl_8;
	MethodInfo_t* ___method_info_9;
	MethodInfo_t* ___original_method_info_10;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data_11;
	int32_t ___method_is_virtual_12;
};

// System.Exception
struct Exception_t  : public RuntimeObject
{
	// System.String System.Exception::_className
	String_t* ____className_1;
	// System.String System.Exception::_message
	String_t* ____message_2;
	// System.Collections.IDictionary System.Exception::_data
	RuntimeObject* ____data_3;
	// System.Exception System.Exception::_innerException
	Exception_t* ____innerException_4;
	// System.String System.Exception::_helpURL
	String_t* ____helpURL_5;
	// System.Object System.Exception::_stackTrace
	RuntimeObject* ____stackTrace_6;
	// System.String System.Exception::_stackTraceString
	String_t* ____stackTraceString_7;
	// System.String System.Exception::_remoteStackTraceString
	String_t* ____remoteStackTraceString_8;
	// System.Int32 System.Exception::_remoteStackIndex
	int32_t ____remoteStackIndex_9;
	// System.Object System.Exception::_dynamicMethods
	RuntimeObject* ____dynamicMethods_10;
	// System.Int32 System.Exception::_HResult
	int32_t ____HResult_11;
	// System.String System.Exception::_source
	String_t* ____source_12;
	// System.Runtime.Serialization.SafeSerializationManager System.Exception::_safeSerializationManager
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager_13;
	// System.Diagnostics.StackTrace[] System.Exception::captured_traces
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces_14;
	// System.IntPtr[] System.Exception::native_trace_ips
	IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832* ___native_trace_ips_15;
	// System.Int32 System.Exception::caught_in_unmanaged
	int32_t ___caught_in_unmanaged_16;
};

struct Exception_t_StaticFields
{
	// System.Object System.Exception::s_EDILock
	RuntimeObject* ___s_EDILock_0;
};
// Native definition for P/Invoke marshalling of System.Exception
struct Exception_t_marshaled_pinvoke
{
	char* ____className_1;
	char* ____message_2;
	RuntimeObject* ____data_3;
	Exception_t_marshaled_pinvoke* ____innerException_4;
	char* ____helpURL_5;
	Il2CppIUnknown* ____stackTrace_6;
	char* ____stackTraceString_7;
	char* ____remoteStackTraceString_8;
	int32_t ____remoteStackIndex_9;
	Il2CppIUnknown* ____dynamicMethods_10;
	int32_t ____HResult_11;
	char* ____source_12;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager_13;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces_14;
	Il2CppSafeArray/*NONE*/* ___native_trace_ips_15;
	int32_t ___caught_in_unmanaged_16;
};
// Native definition for COM marshalling of System.Exception
struct Exception_t_marshaled_com
{
	Il2CppChar* ____className_1;
	Il2CppChar* ____message_2;
	RuntimeObject* ____data_3;
	Exception_t_marshaled_com* ____innerException_4;
	Il2CppChar* ____helpURL_5;
	Il2CppIUnknown* ____stackTrace_6;
	Il2CppChar* ____stackTraceString_7;
	Il2CppChar* ____remoteStackTraceString_8;
	int32_t ____remoteStackIndex_9;
	Il2CppIUnknown* ____dynamicMethods_10;
	int32_t ____HResult_11;
	Il2CppChar* ____source_12;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager_13;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces_14;
	Il2CppSafeArray/*NONE*/* ___native_trace_ips_15;
	int32_t ___caught_in_unmanaged_16;
};

// System.Runtime.InteropServices.GCHandle
struct GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC 
{
	// System.IntPtr System.Runtime.InteropServices.GCHandle::handle
	intptr_t ___handle_0;
};

// RBPhys.RBColliderAABB
struct RBColliderAABB_tE95DAB7A58F11BD8ABCA401925E321024B07C1AF 
{
	// System.Boolean RBPhys.RBColliderAABB::isValidAABB
	bool ___isValidAABB_0;
	// UnityEngine.Vector3 RBPhys.RBColliderAABB::<Center>k__BackingField
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___U3CCenterU3Ek__BackingField_1;
	// UnityEngine.Vector3 RBPhys.RBColliderAABB::<Size>k__BackingField
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___U3CSizeU3Ek__BackingField_2;
};
// Native definition for P/Invoke marshalling of RBPhys.RBColliderAABB
struct RBColliderAABB_tE95DAB7A58F11BD8ABCA401925E321024B07C1AF_marshaled_pinvoke
{
	int32_t ___isValidAABB_0;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___U3CCenterU3Ek__BackingField_1;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___U3CSizeU3Ek__BackingField_2;
};
// Native definition for COM marshalling of RBPhys.RBColliderAABB
struct RBColliderAABB_tE95DAB7A58F11BD8ABCA401925E321024B07C1AF_marshaled_com
{
	int32_t ___isValidAABB_0;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___U3CCenterU3Ek__BackingField_1;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___U3CSizeU3Ek__BackingField_2;
};

// System.RuntimeTypeHandle
struct RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B 
{
	// System.IntPtr System.RuntimeTypeHandle::value
	intptr_t ___value_0;
};

// System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>
struct ValueTuple_2_t761B36235D5F97F497A1CBCD077DF3858814FADE 
{
	// T1 System.ValueTuple`2::Item1
	RuntimeObject* ___Item1_0;
	// T2 System.ValueTuple`2::Item2
	RBColliderAABB_tE95DAB7A58F11BD8ABCA401925E321024B07C1AF ___Item2_1;
};

// System.WeakReference`1<System.Object>
struct WeakReference_1_tED795563AD26F795CED3BBCD488AB1694E385BCE  : public RuntimeObject
{
	// System.Runtime.InteropServices.GCHandle System.WeakReference`1::handle
	GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC ___handle_0;
	// System.Boolean System.WeakReference`1::trackResurrection
	bool ___trackResurrection_1;
};

// System.MulticastDelegate
struct MulticastDelegate_t  : public Delegate_t
{
	// System.Delegate[] System.MulticastDelegate::delegates
	DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771* ___delegates_13;
};
// Native definition for P/Invoke marshalling of System.MulticastDelegate
struct MulticastDelegate_t_marshaled_pinvoke : public Delegate_t_marshaled_pinvoke
{
	Delegate_t_marshaled_pinvoke** ___delegates_13;
};
// Native definition for COM marshalling of System.MulticastDelegate
struct MulticastDelegate_t_marshaled_com : public Delegate_t_marshaled_com
{
	Delegate_t_marshaled_com** ___delegates_13;
};

// RBPhys.RBTrajectory
struct RBTrajectory_t20307245964F0550E7AA11CC639C222B77DBDFD9 
{
	// RBPhys.RBColliderAABB RBPhys.RBTrajectory::trajectoryAABB
	RBColliderAABB_tE95DAB7A58F11BD8ABCA401925E321024B07C1AF ___trajectoryAABB_0;
	// System.Boolean RBPhys.RBTrajectory::isValidTrajectory
	bool ___isValidTrajectory_1;
	// RBPhys.RBRigidbody RBPhys.RBTrajectory::rigidbody
	RBRigidbody_t76882C0EBFE95456DDB0C0358162A84DB06239AD* ___rigidbody_2;
	// System.Boolean RBPhys.RBTrajectory::isStatic
	bool ___isStatic_3;
	// RBPhys.RBCollider RBPhys.RBTrajectory::collider
	RBCollider_t23F94AE4EF06C21499A0CED32EADD3F4A6D36E6C* ___collider_4;
	// RBPhys.RBCollider[] RBPhys.RBTrajectory::colliders
	RBColliderU5BU5D_tDE96D161B9B780342B1676DB63FF9DCC477D3B9B* ___colliders_5;
};
// Native definition for P/Invoke marshalling of RBPhys.RBTrajectory
struct RBTrajectory_t20307245964F0550E7AA11CC639C222B77DBDFD9_marshaled_pinvoke
{
	RBColliderAABB_tE95DAB7A58F11BD8ABCA401925E321024B07C1AF_marshaled_pinvoke ___trajectoryAABB_0;
	int32_t ___isValidTrajectory_1;
	RBRigidbody_t76882C0EBFE95456DDB0C0358162A84DB06239AD* ___rigidbody_2;
	int32_t ___isStatic_3;
	RBCollider_t23F94AE4EF06C21499A0CED32EADD3F4A6D36E6C* ___collider_4;
	RBColliderU5BU5D_tDE96D161B9B780342B1676DB63FF9DCC477D3B9B* ___colliders_5;
};
// Native definition for COM marshalling of RBPhys.RBTrajectory
struct RBTrajectory_t20307245964F0550E7AA11CC639C222B77DBDFD9_marshaled_com
{
	RBColliderAABB_tE95DAB7A58F11BD8ABCA401925E321024B07C1AF_marshaled_com ___trajectoryAABB_0;
	int32_t ___isValidTrajectory_1;
	RBRigidbody_t76882C0EBFE95456DDB0C0358162A84DB06239AD* ___rigidbody_2;
	int32_t ___isStatic_3;
	RBCollider_t23F94AE4EF06C21499A0CED32EADD3F4A6D36E6C* ___collider_4;
	RBColliderU5BU5D_tDE96D161B9B780342B1676DB63FF9DCC477D3B9B* ___colliders_5;
};

// System.SystemException
struct SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295  : public Exception_t
{
};

// System.Type
struct Type_t  : public MemberInfo_t
{
	// System.RuntimeTypeHandle System.Type::_impl
	RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B ____impl_8;
};

struct Type_t_StaticFields
{
	// System.Reflection.Binder modreq(System.Runtime.CompilerServices.IsVolatile) System.Type::s_defaultBinder
	Binder_t91BFCE95A7057FADF4D8A1A342AFE52872246235* ___s_defaultBinder_0;
	// System.Char System.Type::Delimiter
	Il2CppChar ___Delimiter_1;
	// System.Type[] System.Type::EmptyTypes
	TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB* ___EmptyTypes_2;
	// System.Object System.Type::Missing
	RuntimeObject* ___Missing_3;
	// System.Reflection.MemberFilter System.Type::FilterAttribute
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterAttribute_4;
	// System.Reflection.MemberFilter System.Type::FilterName
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterName_5;
	// System.Reflection.MemberFilter System.Type::FilterNameIgnoreCase
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterNameIgnoreCase_6;
};

// System.Func`2<System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>,System.Boolean>
struct Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B  : public MulticastDelegate_t
{
};

// System.Func`2<System.Object,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>
struct Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9  : public MulticastDelegate_t
{
};

// System.Func`2<System.Object,System.Boolean>
struct Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00  : public MulticastDelegate_t
{
};

// System.Func`2<System.Object,System.Single>
struct Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12  : public MulticastDelegate_t
{
};

// System.Func`2<System.Single,System.Boolean>
struct Func_2_t49E998685259ADE759F9329BF66F20DE8667006E  : public MulticastDelegate_t
{
};

// System.Func`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,System.Boolean>
struct Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B  : public MulticastDelegate_t
{
};

// System.Func`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>
struct Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0  : public MulticastDelegate_t
{
};

// System.Linq.Enumerable/Iterator`1<System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>
struct Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40  : public RuntimeObject
{
	// System.Int32 System.Linq.Enumerable/Iterator`1::threadId
	int32_t ___threadId_0;
	// System.Int32 System.Linq.Enumerable/Iterator`1::state
	int32_t ___state_1;
	// TSource System.Linq.Enumerable/Iterator`1::current
	ValueTuple_2_t761B36235D5F97F497A1CBCD077DF3858814FADE ___current_2;
};

// System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>
struct ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 
{
	// T1 System.ValueTuple`3::Item1
	RuntimeObject* ___Item1_0;
	// T2 System.ValueTuple`3::Item2
	RBTrajectory_t20307245964F0550E7AA11CC639C222B77DBDFD9 ___Item2_1;
	// T3 System.ValueTuple`3::Item3
	RBTrajectory_t20307245964F0550E7AA11CC639C222B77DBDFD9 ___Item3_2;
};

// System.ArgumentException
struct ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
	// System.String System.ArgumentException::_paramName
	String_t* ____paramName_18;
};

// System.Collections.Generic.List`1/Enumerator<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>>
struct Enumerator_t3ACCE1C7605054D2EF26F63B987FBD7CEA380901 
{
	// System.Collections.Generic.List`1<T> System.Collections.Generic.List`1/Enumerator::_list
	List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE* ____list_0;
	// System.Int32 System.Collections.Generic.List`1/Enumerator::_index
	int32_t ____index_1;
	// System.Int32 System.Collections.Generic.List`1/Enumerator::_version
	int32_t ____version_2;
	// T System.Collections.Generic.List`1/Enumerator::_current
	ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 ____current_3;
};

// System.Func`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>
struct Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B  : public MulticastDelegate_t
{
};

// System.Func`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Boolean>
struct Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12  : public MulticastDelegate_t
{
};

// System.Func`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Object>
struct Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287  : public MulticastDelegate_t
{
};

// System.Func`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Single>
struct Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72  : public MulticastDelegate_t
{
};

// System.Linq.Enumerable/WhereEnumerableIterator`1<System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>
struct WhereEnumerableIterator_1_tB576B4EE76F70B81DDF11FA35260EC589825E7C5  : public Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40
{
	// System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable/WhereEnumerableIterator`1::source
	RuntimeObject* ___source_3;
	// System.Func`2<TSource,System.Boolean> System.Linq.Enumerable/WhereEnumerableIterator`1::predicate
	Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B* ___predicate_4;
	// System.Collections.Generic.IEnumerator`1<TSource> System.Linq.Enumerable/WhereEnumerableIterator`1::enumerator
	RuntimeObject* ___enumerator_5;
};

// System.Linq.Enumerable/WhereSelectArrayIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>
struct WhereSelectArrayIterator_2_t30000EF5692CE8B73879E35CB9EA48B4DCF6EAB5  : public Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40
{
	// TSource[] System.Linq.Enumerable/WhereSelectArrayIterator`2::source
	ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46* ___source_3;
	// System.Func`2<TSource,System.Boolean> System.Linq.Enumerable/WhereSelectArrayIterator`2::predicate
	Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate_4;
	// System.Func`2<TSource,TResult> System.Linq.Enumerable/WhereSelectArrayIterator`2::selector
	Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B* ___selector_5;
	// System.Int32 System.Linq.Enumerable/WhereSelectArrayIterator`2::index
	int32_t ___index_6;
};

// System.Linq.Enumerable/WhereSelectArrayIterator`2<System.Object,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>
struct WhereSelectArrayIterator_2_tB6BDCFF0C2C4DEEFA9D69A00383291FAA9B56E24  : public Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40
{
	// TSource[] System.Linq.Enumerable/WhereSelectArrayIterator`2::source
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___source_3;
	// System.Func`2<TSource,System.Boolean> System.Linq.Enumerable/WhereSelectArrayIterator`2::predicate
	Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate_4;
	// System.Func`2<TSource,TResult> System.Linq.Enumerable/WhereSelectArrayIterator`2::selector
	Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9* ___selector_5;
	// System.Int32 System.Linq.Enumerable/WhereSelectArrayIterator`2::index
	int32_t ___index_6;
};

// System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>
struct WhereSelectEnumerableIterator_2_t4A082C2E79F346CC9DA87508903869069C5C6454  : public Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40
{
	// System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable/WhereSelectEnumerableIterator`2::source
	RuntimeObject* ___source_3;
	// System.Func`2<TSource,System.Boolean> System.Linq.Enumerable/WhereSelectEnumerableIterator`2::predicate
	Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate_4;
	// System.Func`2<TSource,TResult> System.Linq.Enumerable/WhereSelectEnumerableIterator`2::selector
	Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B* ___selector_5;
	// System.Collections.Generic.IEnumerator`1<TSource> System.Linq.Enumerable/WhereSelectEnumerableIterator`2::enumerator
	RuntimeObject* ___enumerator_6;
};

// System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.Object,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>
struct WhereSelectEnumerableIterator_2_t2379CEC793B1983191566FBE7853007579556057  : public Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40
{
	// System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable/WhereSelectEnumerableIterator`2::source
	RuntimeObject* ___source_3;
	// System.Func`2<TSource,System.Boolean> System.Linq.Enumerable/WhereSelectEnumerableIterator`2::predicate
	Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate_4;
	// System.Func`2<TSource,TResult> System.Linq.Enumerable/WhereSelectEnumerableIterator`2::selector
	Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9* ___selector_5;
	// System.Collections.Generic.IEnumerator`1<TSource> System.Linq.Enumerable/WhereSelectEnumerableIterator`2::enumerator
	RuntimeObject* ___enumerator_6;
};

// System.Linq.Enumerable/WhereSelectListIterator`2<System.Object,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>
struct WhereSelectListIterator_2_t6D877164A02A148F4B5F654B133D07C23FA7904A  : public Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40
{
	// System.Collections.Generic.List`1<TSource> System.Linq.Enumerable/WhereSelectListIterator`2::source
	List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* ___source_3;
	// System.Func`2<TSource,System.Boolean> System.Linq.Enumerable/WhereSelectListIterator`2::predicate
	Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate_4;
	// System.Func`2<TSource,TResult> System.Linq.Enumerable/WhereSelectListIterator`2::selector
	Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9* ___selector_5;
	// System.Collections.Generic.List`1/Enumerator<TSource> System.Linq.Enumerable/WhereSelectListIterator`2::enumerator
	Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A ___enumerator_6;
};

// System.ArgumentNullException
struct ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129  : public ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263
{
};

// System.Linq.Enumerable/WhereSelectListIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>
struct WhereSelectListIterator_2_t8F14DC7F95888AA905CF6AE27A6AFC1205DC2933  : public Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40
{
	// System.Collections.Generic.List`1<TSource> System.Linq.Enumerable/WhereSelectListIterator`2::source
	List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE* ___source_3;
	// System.Func`2<TSource,System.Boolean> System.Linq.Enumerable/WhereSelectListIterator`2::predicate
	Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate_4;
	// System.Func`2<TSource,TResult> System.Linq.Enumerable/WhereSelectListIterator`2::selector
	Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B* ___selector_5;
	// System.Collections.Generic.List`1/Enumerator<TSource> System.Linq.Enumerable/WhereSelectListIterator`2::enumerator
	Enumerator_t3ACCE1C7605054D2EF26F63B987FBD7CEA380901 ___enumerator_6;
};

// System.Linq.Enumerable/WhereSelectListIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Object>
struct WhereSelectListIterator_2_tEB04EFC71ED85DC1E14F1FC388A8CCAF2FB25E8B  : public Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA
{
	// System.Collections.Generic.List`1<TSource> System.Linq.Enumerable/WhereSelectListIterator`2::source
	List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE* ___source_3;
	// System.Func`2<TSource,System.Boolean> System.Linq.Enumerable/WhereSelectListIterator`2::predicate
	Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate_4;
	// System.Func`2<TSource,TResult> System.Linq.Enumerable/WhereSelectListIterator`2::selector
	Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287* ___selector_5;
	// System.Collections.Generic.List`1/Enumerator<TSource> System.Linq.Enumerable/WhereSelectListIterator`2::enumerator
	Enumerator_t3ACCE1C7605054D2EF26F63B987FBD7CEA380901 ___enumerator_6;
};

// System.Linq.Enumerable/WhereSelectListIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Single>
struct WhereSelectListIterator_2_t87543D52D098CB5DBA7AF3A904A5A1183466C3C6  : public Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E
{
	// System.Collections.Generic.List`1<TSource> System.Linq.Enumerable/WhereSelectListIterator`2::source
	List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE* ___source_3;
	// System.Func`2<TSource,System.Boolean> System.Linq.Enumerable/WhereSelectListIterator`2::predicate
	Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate_4;
	// System.Func`2<TSource,TResult> System.Linq.Enumerable/WhereSelectListIterator`2::selector
	Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72* ___selector_5;
	// System.Collections.Generic.List`1/Enumerator<TSource> System.Linq.Enumerable/WhereSelectListIterator`2::enumerator
	Enumerator_t3ACCE1C7605054D2EF26F63B987FBD7CEA380901 ___enumerator_6;
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif
// System.Threading.Tasks.Task`1<System.Boolean>[]
struct Task_1U5BU5D_tBA390D600BBF7458FC9BD634B8F6E52B5D753C91  : public RuntimeArray
{
	ALIGN_FIELD (8) Task_1_t824317F4B958F7512E8F7300511752937A6C6043* m_Items[1];

	inline Task_1_t824317F4B958F7512E8F7300511752937A6C6043* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline Task_1_t824317F4B958F7512E8F7300511752937A6C6043** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, Task_1_t824317F4B958F7512E8F7300511752937A6C6043* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline Task_1_t824317F4B958F7512E8F7300511752937A6C6043* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline Task_1_t824317F4B958F7512E8F7300511752937A6C6043** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, Task_1_t824317F4B958F7512E8F7300511752937A6C6043* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};
// System.Boolean[]
struct BooleanU5BU5D_tD317D27C31DB892BE79FAE3AEBC0B3FFB73DE9B4  : public RuntimeArray
{
	ALIGN_FIELD (8) bool m_Items[1];

	inline bool GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline bool* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, bool value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline bool GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline bool* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, bool value)
	{
		m_Items[index] = value;
	}
};
// System.Threading.Tasks.Task[]
struct TaskU5BU5D_t368E447BD9A179BA9A26BAAABF1BAE9CA79F60B3  : public RuntimeArray
{
	ALIGN_FIELD (8) Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* m_Items[1];

	inline Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};
// System.Threading.Tasks.Task`1<System.Object>[]
struct Task_1U5BU5D_tE516FE52659CC40D166035B14BAEFD6B275FBA1E  : public RuntimeArray
{
	ALIGN_FIELD (8) Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2* m_Items[1];

	inline Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};
// System.Object[]
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918  : public RuntimeArray
{
	ALIGN_FIELD (8) RuntimeObject* m_Items[1];

	inline RuntimeObject* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline RuntimeObject** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, RuntimeObject* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline RuntimeObject* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline RuntimeObject** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, RuntimeObject* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};
// System.Threading.Tasks.Task`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>[]
struct Task_1U5BU5D_t46133A6F873A9EC9FC75DAB1315ABC8025565F09  : public RuntimeArray
{
	ALIGN_FIELD (8) Task_1_tDF1FF540D7D2248A08580387A39717B7FB7A9CF9* m_Items[1];

	inline Task_1_tDF1FF540D7D2248A08580387A39717B7FB7A9CF9* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline Task_1_tDF1FF540D7D2248A08580387A39717B7FB7A9CF9** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, Task_1_tDF1FF540D7D2248A08580387A39717B7FB7A9CF9* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline Task_1_tDF1FF540D7D2248A08580387A39717B7FB7A9CF9* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline Task_1_tDF1FF540D7D2248A08580387A39717B7FB7A9CF9** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, Task_1_tDF1FF540D7D2248A08580387A39717B7FB7A9CF9* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};
// Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType[]
struct __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC  : public RuntimeArray
{
	ALIGN_FIELD (8) uint8_t m_Items[1];

	inline uint8_t* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + il2cpp_array_calc_byte_offset(this, index);
	}
	inline uint8_t* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + il2cpp_array_calc_byte_offset(this, index);
	}
};
// System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>[]
struct ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46  : public RuntimeArray
{
	ALIGN_FIELD (8) ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 m_Items[1];

	inline ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)&((m_Items + index)->___Item1_0), (void*)NULL);
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&((m_Items + index)->___Item2_1))->___rigidbody_2), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&((m_Items + index)->___Item2_1))->___collider_4), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&((m_Items + index)->___Item2_1))->___colliders_5), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&((m_Items + index)->___Item3_2))->___rigidbody_2), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&((m_Items + index)->___Item3_2))->___collider_4), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&((m_Items + index)->___Item3_2))->___colliders_5), (void*)NULL);
		#endif
	}
	inline ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)&((m_Items + index)->___Item1_0), (void*)NULL);
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&((m_Items + index)->___Item2_1))->___rigidbody_2), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&((m_Items + index)->___Item2_1))->___collider_4), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&((m_Items + index)->___Item2_1))->___colliders_5), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&((m_Items + index)->___Item3_2))->___rigidbody_2), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&((m_Items + index)->___Item3_2))->___collider_4), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&((m_Items + index)->___Item3_2))->___colliders_5), (void*)NULL);
		#endif
	}
};


// System.Void System.WeakReference`1<System.Object>::.ctor(T,System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WeakReference_1__ctor_m99141AB321E022D9933448CDD7139BC9FAA443E8_gshared (WeakReference_1_tED795563AD26F795CED3BBCD488AB1694E385BCE* __this, RuntimeObject* ___target0, bool ___trackResurrection1, const RuntimeMethod* method) ;
// System.Void System.Threading.Tasks.Task`1<System.Object>::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Task_1__ctor_m2A3A26E17D6BE69CEEC048C0599F76FE4C6D24A0_gshared (Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2* __this, const RuntimeMethod* method) ;
// System.Void System.Threading.Tasks.Task/WhenAllPromise`1<System.Boolean>::Invoke(System.Threading.Tasks.Task)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhenAllPromise_1_Invoke_m1AE233CB3A4177EE300CDAC3308A625AC299A5D4_gshared (WhenAllPromise_1_t97739DA26ABE51AE956DAFF6EB494F6A4F190235* __this, Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* ___ignored0, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.LowLevelListWithIList`1<System.Object>::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void LowLevelListWithIList_1__ctor_mD6F74009D6D4AFB9BB96C40001514D52B63DED1F_gshared (LowLevelListWithIList_1_t424B84BB083921C00880052D4B49074AF66B72FC* __this, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.LowLevelList`1<System.Object>::AddRange(System.Collections.Generic.IEnumerable`1<T>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void LowLevelList_1_AddRange_m9D986B88377A55637A9383B0DD0DF626A7ED2042_gshared (LowLevelList_1_tFB82D019B54AD98BC47D01C80B32C9DC3FA3BE58* __this, RuntimeObject* ___collection0, const RuntimeMethod* method) ;
// TResult System.Threading.Tasks.Task`1<System.Boolean>::GetResultCore(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Task_1_GetResultCore_mCE0DE2898E40C4E670E13C134F029C59AC4EA2FB_gshared (Task_1_t824317F4B958F7512E8F7300511752937A6C6043* __this, bool ___waitCompletionNotification0, const RuntimeMethod* method) ;
// System.Boolean System.Threading.Tasks.Task`1<System.Object>::TrySetResult(TResult)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Task_1_TrySetResult_m2EE766FD3F76F4824990F4A93ED1F7253ECE014C_gshared (Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2* __this, RuntimeObject* ___result0, const RuntimeMethod* method) ;
// System.Void System.Threading.Tasks.Task/WhenAllPromise`1<System.Object>::Invoke(System.Threading.Tasks.Task)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhenAllPromise_1_Invoke_mC22438FD090403404288BA997992DE80EE2FA153_gshared (WhenAllPromise_1_t6B7D3ABF753EA8F6F7581C787A300C50CBA54FF0* __this, Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* ___ignored0, const RuntimeMethod* method) ;
// TResult System.Threading.Tasks.Task`1<System.Object>::GetResultCore(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Task_1_GetResultCore_mA6141C79F2DD78034AE5BC0311D4D143A5C320B9_gshared (Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2* __this, bool ___waitCompletionNotification0, const RuntimeMethod* method) ;
// System.Void System.Linq.Enumerable/Iterator`1<System.Object>::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Iterator_1__ctor_m3E47867714E05673E54C6B73D9242F5FFADA1F63_gshared (Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA* __this, const RuntimeMethod* method) ;
// System.Void System.Linq.Enumerable/WhereArrayIterator`1<System.Object>::.ctor(TSource[],System.Func`2<TSource,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereArrayIterator_1__ctor_mC17BAA23BA92C7455512FDA5B1618C6D2B54ACA2_gshared (WhereArrayIterator_1_t027D2511F9B69346688FE3E5623EF2BEE81E9FAA* __this, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___source0, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate1, const RuntimeMethod* method) ;
// TResult System.Func`2<System.Object,System.Boolean>::Invoke(T)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Func_2_Invoke_m2014423FB900F135C8FF994125604FF9E6AAE829_gshared_inline (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* __this, RuntimeObject* ___arg0, const RuntimeMethod* method) ;
// System.Func`2<TSource,System.Boolean> System.Linq.Enumerable::CombinePredicates<System.Object>(System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* Enumerable_CombinePredicates_TisRuntimeObject_m613479C29B013E8FC2987E22F42A3BC6CC2C9768_gshared (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate10, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate21, const RuntimeMethod* method) ;
// System.Void System.Linq.Enumerable/Iterator`1<System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Iterator_1__ctor_m27CEA132FC2E7D7BA6A8529CE0647F2709FC29A3_gshared (Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40* __this, const RuntimeMethod* method) ;
// System.Void System.Linq.Enumerable/WhereEnumerableIterator`1<System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::.ctor(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereEnumerableIterator_1__ctor_mC87BD639674AE32961C0AC5A5878F4144E7DA780_gshared (WhereEnumerableIterator_1_tB576B4EE76F70B81DDF11FA35260EC589825E7C5* __this, RuntimeObject* ___source0, Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B* ___predicate1, const RuntimeMethod* method) ;
// System.Void System.Linq.Enumerable/Iterator`1<System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::Dispose()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Iterator_1_Dispose_m51BD0D9CCB4CB032C42DA20F3C867462771689AB_gshared (Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40* __this, const RuntimeMethod* method) ;
// TResult System.Func`2<System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>,System.Boolean>::Invoke(T)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Func_2_Invoke_m1DC9E7C418A082CE71CE5A7F12B8F8F30528804F_gshared_inline (Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B* __this, ValueTuple_2_t761B36235D5F97F497A1CBCD077DF3858814FADE ___arg0, const RuntimeMethod* method) ;
// System.Func`2<TSource,System.Boolean> System.Linq.Enumerable::CombinePredicates<System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>(System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B* Enumerable_CombinePredicates_TisValueTuple_2_t761B36235D5F97F497A1CBCD077DF3858814FADE_m97FA1ECA0B2F0FA21E3CF45AAEB7C5157CC47603_gshared (Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B* ___predicate10, Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B* ___predicate21, const RuntimeMethod* method) ;
// System.Void System.Linq.Enumerable/WhereEnumerableIterator`1<System.Object>::.ctor(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereEnumerableIterator_1__ctor_m8C0DA4CDA5431C03561F67C4393BB18CDD891F01_gshared (WhereEnumerableIterator_1_t1E787D13759F5A31C94B3FAED181402B25C278F4* __this, RuntimeObject* ___source0, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate1, const RuntimeMethod* method) ;
// System.Void System.Linq.Enumerable/Iterator`1<System.Object>::Dispose()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Iterator_1_Dispose_m953BCF886C8A63456821023DBA45EBD9AC44EB07_gshared (Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA* __this, const RuntimeMethod* method) ;
// System.Void System.Linq.Enumerable/Iterator`1<System.Single>::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Iterator_1__ctor_m7FEBAE985CDE5DD1CB255EB4A3D65F05AA8D2AEF_gshared (Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E* __this, const RuntimeMethod* method) ;
// System.Void System.Linq.Enumerable/WhereEnumerableIterator`1<System.Single>::.ctor(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereEnumerableIterator_1__ctor_mCB49A03958EF827EF6CE47402259941EAB31D984_gshared (WhereEnumerableIterator_1_t7BB2D1D9F8A6E52243A87F45DFEEA4209D685F7C* __this, RuntimeObject* ___source0, Func_2_t49E998685259ADE759F9329BF66F20DE8667006E* ___predicate1, const RuntimeMethod* method) ;
// System.Void System.Linq.Enumerable/Iterator`1<System.Single>::Dispose()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Iterator_1_Dispose_mC0A88E7EEED5A9CFDCACF160DE8F58A713DFF222_gshared (Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E* __this, const RuntimeMethod* method) ;
// TResult System.Func`2<System.Single,System.Boolean>::Invoke(T)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Func_2_Invoke_m1FE6F2A4EC23CC595897C55AE7B0BDA8969044D7_gshared_inline (Func_2_t49E998685259ADE759F9329BF66F20DE8667006E* __this, float ___arg0, const RuntimeMethod* method) ;
// System.Func`2<TSource,System.Boolean> System.Linq.Enumerable::CombinePredicates<System.Single>(System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Func_2_t49E998685259ADE759F9329BF66F20DE8667006E* Enumerable_CombinePredicates_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_m2F9BC4BEE0498F9B3486BA254B507C1FCE89A763_gshared (Func_2_t49E998685259ADE759F9329BF66F20DE8667006E* ___predicate10, Func_2_t49E998685259ADE759F9329BF66F20DE8667006E* ___predicate21, const RuntimeMethod* method) ;
// System.Void System.Linq.Enumerable/WhereListIterator`1<System.Object>::.ctor(System.Collections.Generic.List`1<TSource>,System.Func`2<TSource,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereListIterator_1__ctor_mEF8F62B9078E538C1DC46BCB876C2AC5B29EA73B_gshared (WhereListIterator_1_t1F40F08BAF8586F2C09294085BC605CC2FA432EB* __this, List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* ___source0, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate1, const RuntimeMethod* method) ;
// System.Collections.Generic.List`1/Enumerator<T> System.Collections.Generic.List`1<System.Object>::GetEnumerator()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A List_1_GetEnumerator_mD8294A7FA2BEB1929487127D476F8EC1CDC23BFC_gshared (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method) ;
// T System.Collections.Generic.List`1/Enumerator<System.Object>::get_Current()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_get_Current_m6330F15D18EE4F547C05DF9BF83C5EB710376027_gshared_inline (Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A* __this, const RuntimeMethod* method) ;
// System.Boolean System.Collections.Generic.List`1/Enumerator<System.Object>::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_mE921CC8F29FBBDE7CC3209A0ED0D921D58D00BCB_gshared (Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A* __this, const RuntimeMethod* method) ;
// System.Void System.Linq.Enumerable/WhereSelectArrayIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::.ctor(TSource[],System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectArrayIterator_2__ctor_m5DCBBB86A9209D1F91CCB6D656EDCD6A2AF93A31_gshared (WhereSelectArrayIterator_2_t30000EF5692CE8B73879E35CB9EA48B4DCF6EAB5* __this, ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46* ___source0, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate1, Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B* ___selector2, const RuntimeMethod* method) ;
// TResult System.Func`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Boolean>::Invoke(T)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Func_2_Invoke_m12B4F5C7D4C30A85BD18305B0CDE8DF42312F402_gshared_inline (Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* __this, ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 ___arg0, const RuntimeMethod* method) ;
// TResult System.Func`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::Invoke(T)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ValueTuple_2_t761B36235D5F97F497A1CBCD077DF3858814FADE Func_2_Invoke_m3794F6EAED760CA17FC7A24439DC423AC6BF8FB9_gshared_inline (Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B* __this, ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 ___arg0, const RuntimeMethod* method) ;
// System.Void System.Linq.Enumerable/WhereSelectArrayIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Object>::.ctor(TSource[],System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectArrayIterator_2__ctor_m7932AB67118B332F6FAEB44AA075721F4C0BC26B_gshared (WhereSelectArrayIterator_2_tBABE519E8262C3B19F12FA57F0EBBEF62306E2EE* __this, ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46* ___source0, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate1, Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287* ___selector2, const RuntimeMethod* method) ;
// TResult System.Func`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Object>::Invoke(T)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* Func_2_Invoke_m9EB50B06293708FB57A5A30A460483868FFF99D0_gshared_inline (Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287* __this, ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 ___arg0, const RuntimeMethod* method) ;
// System.Void System.Linq.Enumerable/WhereSelectArrayIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Single>::.ctor(TSource[],System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectArrayIterator_2__ctor_mA507C5E2A96C465B089EC01D8C9D02D9D5D3D0E4_gshared (WhereSelectArrayIterator_2_tFF39CC0F264CA7C80FEA868C00FD12A1D45DC58F* __this, ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46* ___source0, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate1, Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72* ___selector2, const RuntimeMethod* method) ;
// TResult System.Func`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Single>::Invoke(T)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Func_2_Invoke_mC888CBA13A68639AC714989A4F3DC91C432BB307_gshared_inline (Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72* __this, ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 ___arg0, const RuntimeMethod* method) ;
// System.Void System.Linq.Enumerable/WhereSelectArrayIterator`2<System.Object,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::.ctor(TSource[],System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectArrayIterator_2__ctor_m542DF3BD7D0591BD1A6CF2AC84E55CB5431BBDD7_gshared (WhereSelectArrayIterator_2_tB6BDCFF0C2C4DEEFA9D69A00383291FAA9B56E24* __this, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___source0, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate1, Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9* ___selector2, const RuntimeMethod* method) ;
// TResult System.Func`2<System.Object,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::Invoke(T)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ValueTuple_2_t761B36235D5F97F497A1CBCD077DF3858814FADE Func_2_Invoke_m30EE337C21F1CA674046A64A8A2DA68A02701634_gshared_inline (Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9* __this, RuntimeObject* ___arg0, const RuntimeMethod* method) ;
// System.Void System.Linq.Enumerable/WhereSelectArrayIterator`2<System.Object,System.Single>::.ctor(TSource[],System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectArrayIterator_2__ctor_m98F1E0E34745862A883CE6A1CCE4DE42E16CF8A9_gshared (WhereSelectArrayIterator_2_tD87299FD8280F38D5E50B5174574F2F20188E0D4* __this, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___source0, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate1, Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12* ___selector2, const RuntimeMethod* method) ;
// TResult System.Func`2<System.Object,System.Single>::Invoke(T)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Func_2_Invoke_mBE16A5FDA5E80CCBA51D69334EF21C0F03D353AF_gshared_inline (Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12* __this, RuntimeObject* ___arg0, const RuntimeMethod* method) ;
// System.Void System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::.ctor(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectEnumerableIterator_2__ctor_mA080BFD551E780DEE7C4C1CFE7BAAA6662478900_gshared (WhereSelectEnumerableIterator_2_t4A082C2E79F346CC9DA87508903869069C5C6454* __this, RuntimeObject* ___source0, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate1, Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B* ___selector2, const RuntimeMethod* method) ;
// System.Void System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Object>::.ctor(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectEnumerableIterator_2__ctor_mA9895FAF25F9C9A7894DD268E375875F8FBA6E76_gshared (WhereSelectEnumerableIterator_2_t27953926A9605639C866748EA3EF6801E63B5430* __this, RuntimeObject* ___source0, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate1, Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287* ___selector2, const RuntimeMethod* method) ;
// System.Void System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Single>::.ctor(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectEnumerableIterator_2__ctor_m00D2CEA2D2B5A0C3A03E1A7A7F9BBA5229DEF1B2_gshared (WhereSelectEnumerableIterator_2_tA24C2F75772C5AB98B395DC9C70CDD82327A82C4* __this, RuntimeObject* ___source0, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate1, Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72* ___selector2, const RuntimeMethod* method) ;
// System.Void System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.Object,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::.ctor(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectEnumerableIterator_2__ctor_m5538389BC390D50DBDA10D3C0945A79D830641B3_gshared (WhereSelectEnumerableIterator_2_t2379CEC793B1983191566FBE7853007579556057* __this, RuntimeObject* ___source0, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate1, Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9* ___selector2, const RuntimeMethod* method) ;
// System.Void System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.Object,System.Single>::.ctor(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectEnumerableIterator_2__ctor_mE8B29F90A4CD398E256C48F9451231F2EE02008E_gshared (WhereSelectEnumerableIterator_2_tF7B17AE20C591499CCC69CF4CFEA521F28D66E94* __this, RuntimeObject* ___source0, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate1, Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12* ___selector2, const RuntimeMethod* method) ;
// System.Void System.Linq.Enumerable/WhereSelectListIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::.ctor(System.Collections.Generic.List`1<TSource>,System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectListIterator_2__ctor_m8AEB85EB9B265A2F69C0BE789DA26BA006C9E903_gshared (WhereSelectListIterator_2_t8F14DC7F95888AA905CF6AE27A6AFC1205DC2933* __this, List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE* ___source0, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate1, Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B* ___selector2, const RuntimeMethod* method) ;
// System.Collections.Generic.List`1/Enumerator<T> System.Collections.Generic.List`1<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>>::GetEnumerator()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Enumerator_t3ACCE1C7605054D2EF26F63B987FBD7CEA380901 List_1_GetEnumerator_m180F98F72F5DA91BF8696E010117148062025D82_gshared (List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE* __this, const RuntimeMethod* method) ;
// T System.Collections.Generic.List`1/Enumerator<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>>::get_Current()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 Enumerator_get_Current_mE43C1A4AD61C4A5D58C7E5F6D98CD7DE40A64987_gshared_inline (Enumerator_t3ACCE1C7605054D2EF26F63B987FBD7CEA380901* __this, const RuntimeMethod* method) ;
// System.Boolean System.Collections.Generic.List`1/Enumerator<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>>::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_mD0B6A0F10BA08A58F39285C8C8309111A79A3F7A_gshared (Enumerator_t3ACCE1C7605054D2EF26F63B987FBD7CEA380901* __this, const RuntimeMethod* method) ;
// System.Void System.Linq.Enumerable/WhereSelectListIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Object>::.ctor(System.Collections.Generic.List`1<TSource>,System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectListIterator_2__ctor_mB2F43B2048C674205F65D38CD3AD6FE74C08FBE0_gshared (WhereSelectListIterator_2_tEB04EFC71ED85DC1E14F1FC388A8CCAF2FB25E8B* __this, List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE* ___source0, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate1, Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287* ___selector2, const RuntimeMethod* method) ;
// System.Void System.Linq.Enumerable/WhereSelectListIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Single>::.ctor(System.Collections.Generic.List`1<TSource>,System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectListIterator_2__ctor_m0889BA82694536AAAE4B2C793DC8825399744C3D_gshared (WhereSelectListIterator_2_t87543D52D098CB5DBA7AF3A904A5A1183466C3C6* __this, List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE* ___source0, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate1, Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72* ___selector2, const RuntimeMethod* method) ;
// System.Void System.Linq.Enumerable/WhereSelectListIterator`2<System.Object,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::.ctor(System.Collections.Generic.List`1<TSource>,System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectListIterator_2__ctor_m6F364DC8CF50F39787083DBD5334AC40433CC8B8_gshared (WhereSelectListIterator_2_t6D877164A02A148F4B5F654B133D07C23FA7904A* __this, List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* ___source0, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate1, Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9* ___selector2, const RuntimeMethod* method) ;
// System.Void System.Linq.Enumerable/WhereSelectListIterator`2<System.Object,System.Single>::.ctor(System.Collections.Generic.List`1<TSource>,System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectListIterator_2__ctor_m22BAFF678F9378729DD9E5551BBA6BA69FDEEF94_gshared (WhereSelectListIterator_2_tFE32164179EE37B666D799CFB7F5167FB4C464D9* __this, List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* ___source0, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate1, Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12* ___selector2, const RuntimeMethod* method) ;

// System.Void System.WeakReference`1<System.Object>::.ctor(T,System.Boolean)
inline void WeakReference_1__ctor_m99141AB321E022D9933448CDD7139BC9FAA443E8 (WeakReference_1_tED795563AD26F795CED3BBCD488AB1694E385BCE* __this, RuntimeObject* ___target0, bool ___trackResurrection1, const RuntimeMethod* method)
{
	((  void (*) (WeakReference_1_tED795563AD26F795CED3BBCD488AB1694E385BCE*, RuntimeObject*, bool, const RuntimeMethod*))WeakReference_1__ctor_m99141AB321E022D9933448CDD7139BC9FAA443E8_gshared)(__this, ___target0, ___trackResurrection1, method);
}
// System.Void System.Object::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2 (RuntimeObject* __this, const RuntimeMethod* method) ;
// System.Runtime.InteropServices.GCHandle System.Runtime.InteropServices.GCHandle::Alloc(System.Object,System.Runtime.InteropServices.GCHandleType)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC GCHandle_Alloc_m3BFD398427352FC756FFE078F01A504B681352EC (RuntimeObject* ___value0, int32_t ___type1, const RuntimeMethod* method) ;
// System.Void System.ArgumentNullException::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ArgumentNullException__ctor_m444AE141157E333844FC1A9500224C2F9FD24F4B (ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129* __this, String_t* ___paramName0, const RuntimeMethod* method) ;
// System.Boolean System.Runtime.Serialization.SerializationInfo::GetBoolean(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool SerializationInfo_GetBoolean_m8335F8E11B572AB6B5BF85A9355D6888D5847EF5 (SerializationInfo_t3C47F63E24BEB9FCE2DC6309E027F238DC5C5E37* __this, String_t* ___name0, const RuntimeMethod* method) ;
// System.Type System.Type::GetTypeFromHandle(System.RuntimeTypeHandle)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Type_t* Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57 (RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B ___handle0, const RuntimeMethod* method) ;
// System.Object System.Runtime.Serialization.SerializationInfo::GetValue(System.String,System.Type)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* SerializationInfo_GetValue_mE6091C2E906E113455D05E734C86F43B8E1D1034 (SerializationInfo_t3C47F63E24BEB9FCE2DC6309E027F238DC5C5E37* __this, String_t* ___name0, Type_t* ___type1, const RuntimeMethod* method) ;
// System.Void System.Runtime.Serialization.SerializationInfo::AddValue(System.String,System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SerializationInfo_AddValue_mC52253CB19C98F82A26E32C941F8F20E106D4C0D (SerializationInfo_t3C47F63E24BEB9FCE2DC6309E027F238DC5C5E37* __this, String_t* ___name0, bool ___value1, const RuntimeMethod* method) ;
// System.Boolean System.Runtime.InteropServices.GCHandle::get_IsAllocated()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool GCHandle_get_IsAllocated_m241908103D8D867E11CCAB73C918729825E86843 (GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC* __this, const RuntimeMethod* method) ;
// System.Object System.Runtime.InteropServices.GCHandle::get_Target()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* GCHandle_get_Target_m481F9508DA5E384D33CD1F4450060DC56BBD4CD5 (GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC* __this, const RuntimeMethod* method) ;
// System.Void System.Runtime.Serialization.SerializationInfo::AddValue(System.String,System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SerializationInfo_AddValue_m28FE9B110F21DDB8FF5F5E35A0EABD659DB22C2F (SerializationInfo_t3C47F63E24BEB9FCE2DC6309E027F238DC5C5E37* __this, String_t* ___name0, RuntimeObject* ___value1, const RuntimeMethod* method) ;
// System.Void System.Object::Finalize()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object_Finalize_mC98C96301CCABFE00F1A7EF8E15DF507CACD42B2 (RuntimeObject* __this, const RuntimeMethod* method) ;
// System.Void System.Runtime.InteropServices.GCHandle::Free()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void GCHandle_Free_m1320A260E487EB1EA6D95F9E54BFFCB5A4EF83A3 (GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC* __this, const RuntimeMethod* method) ;
// System.Void System.Threading.Tasks.Task`1<System.Boolean[]>::.ctor()
inline void Task_1__ctor_m7894EC8EE695D23CC55A13A5303EBB5CC53FB12F (Task_1_t69C91E4C5122CE3A171C7090781C6B14BE5D63DA* __this, const RuntimeMethod* method)
{
	((  void (*) (Task_1_t69C91E4C5122CE3A171C7090781C6B14BE5D63DA*, const RuntimeMethod*))Task_1__ctor_m2A3A26E17D6BE69CEEC048C0599F76FE4C6D24A0_gshared)(__this, method);
}
// System.Boolean System.Threading.Tasks.DebuggerSupport::get_LoggingOn()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool DebuggerSupport_get_LoggingOn_mD838646A5A048C62BAB034257EF0F2F852AF0ABB (const RuntimeMethod* method) ;
// System.Void System.Threading.Tasks.DebuggerSupport::TraceOperationCreation(System.Threading.Tasks.CausalityTraceLevel,System.Threading.Tasks.Task,System.String,System.UInt64)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DebuggerSupport_TraceOperationCreation_m311097028455DBEED9480F6315649693464F2C06 (int32_t ___traceLevel0, Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* ___task1, String_t* ___operationName2, uint64_t ___relatedContext3, const RuntimeMethod* method) ;
// System.Void System.Threading.Tasks.DebuggerSupport::AddToActiveTasks(System.Threading.Tasks.Task)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void DebuggerSupport_AddToActiveTasks_mF592C309CB2AE9C85563BE114DDDB6D226AD9E3E_inline (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* ___task0, const RuntimeMethod* method) ;
// System.Boolean System.Threading.Tasks.Task::get_IsCompleted()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Task_get_IsCompleted_m942D6D536545EF059089398B19435591561BB831 (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* __this, const RuntimeMethod* method) ;
// System.Void System.Threading.Tasks.Task/WhenAllPromise`1<System.Boolean>::Invoke(System.Threading.Tasks.Task)
inline void WhenAllPromise_1_Invoke_m1AE233CB3A4177EE300CDAC3308A625AC299A5D4 (WhenAllPromise_1_t97739DA26ABE51AE956DAFF6EB494F6A4F190235* __this, Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* ___ignored0, const RuntimeMethod* method)
{
	((  void (*) (WhenAllPromise_1_t97739DA26ABE51AE956DAFF6EB494F6A4F190235*, Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*, const RuntimeMethod*))WhenAllPromise_1_Invoke_m1AE233CB3A4177EE300CDAC3308A625AC299A5D4_gshared)(__this, ___ignored0, method);
}
// System.Void System.Threading.Tasks.Task::AddCompletionAction(System.Threading.Tasks.ITaskCompletionAction)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Task_AddCompletionAction_m77811E563FC391FF0F51DD14AC67D35318378CDA (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* __this, RuntimeObject* ___action0, const RuntimeMethod* method) ;
// System.Void System.Threading.Tasks.DebuggerSupport::TraceOperationRelation(System.Threading.Tasks.CausalityTraceLevel,System.Threading.Tasks.Task,System.Threading.Tasks.CausalityRelation)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DebuggerSupport_TraceOperationRelation_m940A2FF274D08177EA7D83FAAE4492DAC0CFE16D (int32_t ___traceLevel0, Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* ___task1, int32_t ___relation2, const RuntimeMethod* method) ;
// System.Int32 System.Threading.Interlocked::Decrement(System.Int32&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Interlocked_Decrement_m6AFAD2E874CBDA373B1EF7572F11D6E91813E75D (int32_t* ___location0, const RuntimeMethod* method) ;
// System.Boolean System.Threading.Tasks.Task::get_IsFaulted()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Task_get_IsFaulted_mC0AD3EA4EAF3B47C1F5FE9624541F0A00B9426D9 (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* __this, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.LowLevelListWithIList`1<System.Runtime.ExceptionServices.ExceptionDispatchInfo>::.ctor()
inline void LowLevelListWithIList_1__ctor_m0E3A007F2F2CA220DB5C20F85BD60FE2F4A045E2 (LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4* __this, const RuntimeMethod* method)
{
	((  void (*) (LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4*, const RuntimeMethod*))LowLevelListWithIList_1__ctor_mD6F74009D6D4AFB9BB96C40001514D52B63DED1F_gshared)(__this, method);
}
// System.Collections.ObjectModel.ReadOnlyCollection`1<System.Runtime.ExceptionServices.ExceptionDispatchInfo> System.Threading.Tasks.Task::GetExceptionDispatchInfos()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlyCollection_1_t7E3BC8E94E9BC82C2FD0D77A76BF08AC79C2CECD* Task_GetExceptionDispatchInfos_m2E8811FF2E0CDBC4BFE281A4822C6D8452832831 (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* __this, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.LowLevelList`1<System.Runtime.ExceptionServices.ExceptionDispatchInfo>::AddRange(System.Collections.Generic.IEnumerable`1<T>)
inline void LowLevelList_1_AddRange_mD5D6366F78D1A613E03AE6A45654DE4C2A6C51BE (LowLevelList_1_tD1EA453996325637EAF8C2A867D4DED46603298F* __this, RuntimeObject* ___collection0, const RuntimeMethod* method)
{
	((  void (*) (LowLevelList_1_tD1EA453996325637EAF8C2A867D4DED46603298F*, RuntimeObject*, const RuntimeMethod*))LowLevelList_1_AddRange_m9D986B88377A55637A9383B0DD0DF626A7ED2042_gshared)(__this, ___collection0, method);
}
// System.Boolean System.Threading.Tasks.Task::get_IsCanceled()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Task_get_IsCanceled_m96A8D3F85158A9CB3AEA50A00A55BE4E0F0E21FA (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* __this, const RuntimeMethod* method) ;
// TResult System.Threading.Tasks.Task`1<System.Boolean>::GetResultCore(System.Boolean)
inline bool Task_1_GetResultCore_mCE0DE2898E40C4E670E13C134F029C59AC4EA2FB (Task_1_t824317F4B958F7512E8F7300511752937A6C6043* __this, bool ___waitCompletionNotification0, const RuntimeMethod* method)
{
	return ((  bool (*) (Task_1_t824317F4B958F7512E8F7300511752937A6C6043*, bool, const RuntimeMethod*))Task_1_GetResultCore_mCE0DE2898E40C4E670E13C134F029C59AC4EA2FB_gshared)(__this, ___waitCompletionNotification0, method);
}
// System.Boolean System.Threading.Tasks.Task::get_IsWaitNotificationEnabled()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Task_get_IsWaitNotificationEnabled_mF6950E2B28561EE2E57DECADAD63B485CA5DD3A8 (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* __this, const RuntimeMethod* method) ;
// System.Void System.Threading.Tasks.Task::SetNotificationForWaitCompletion(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Task_SetNotificationForWaitCompletion_m6B087B3B1E1B6911006874042808D4D7D9678AED (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* __this, bool ___enabled0, const RuntimeMethod* method) ;
// System.Boolean System.Threading.Tasks.Task::TrySetException(System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Task_TrySetException_m8336BA31D11EA84916A89EB8A7A0044D2D0EE94D (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* __this, RuntimeObject* ___exceptionObject0, const RuntimeMethod* method) ;
// System.Threading.CancellationToken System.Threading.Tasks.Task::get_CancellationToken()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR CancellationToken_t51142D9C6D7C02D314DA34A6A7988C528992FFED Task_get_CancellationToken_m459E6E4311018E389AC44E089CCB4ACDC252766A (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* __this, const RuntimeMethod* method) ;
// System.Runtime.ExceptionServices.ExceptionDispatchInfo System.Threading.Tasks.Task::GetCancellationExceptionDispatchInfo()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ExceptionDispatchInfo_tD7AF19E75FEC22F4A8329FD1E9EDF96615CB2757* Task_GetCancellationExceptionDispatchInfo_m190A98B306C8BCCB67F3D8B2E7B8BF75EAE63E34 (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* __this, const RuntimeMethod* method) ;
// System.Boolean System.Threading.Tasks.Task::TrySetCanceled(System.Threading.CancellationToken,System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Task_TrySetCanceled_m8E24757A8DD3AE5A856B64D87B447E08395A0771 (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* __this, CancellationToken_t51142D9C6D7C02D314DA34A6A7988C528992FFED ___tokenToRecord0, RuntimeObject* ___cancellationException1, const RuntimeMethod* method) ;
// System.Void System.Threading.Tasks.DebuggerSupport::TraceOperationCompletion(System.Threading.Tasks.CausalityTraceLevel,System.Threading.Tasks.Task,Internal.Runtime.Augments.AsyncStatus)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DebuggerSupport_TraceOperationCompletion_m7047A96BCB7DC4835B38D1B965B4BC3049AF62CB (int32_t ___traceLevel0, Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* ___task1, int32_t ___status2, const RuntimeMethod* method) ;
// System.Void System.Threading.Tasks.DebuggerSupport::RemoveFromActiveTasks(System.Threading.Tasks.Task)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void DebuggerSupport_RemoveFromActiveTasks_m19229D30DAA2447DDAB524F2A0E9718D070A1251_inline (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* ___task0, const RuntimeMethod* method) ;
// System.Boolean System.Threading.Tasks.Task`1<System.Boolean[]>::TrySetResult(TResult)
inline bool Task_1_TrySetResult_m69CB8B64C8114D46D75D13BA20D79AEE9C6FB159 (Task_1_t69C91E4C5122CE3A171C7090781C6B14BE5D63DA* __this, BooleanU5BU5D_tD317D27C31DB892BE79FAE3AEBC0B3FFB73DE9B4* ___result0, const RuntimeMethod* method)
{
	return ((  bool (*) (Task_1_t69C91E4C5122CE3A171C7090781C6B14BE5D63DA*, BooleanU5BU5D_tD317D27C31DB892BE79FAE3AEBC0B3FFB73DE9B4*, const RuntimeMethod*))Task_1_TrySetResult_m2EE766FD3F76F4824990F4A93ED1F7253ECE014C_gshared)(__this, ___result0, method);
}
// System.Boolean System.Threading.Tasks.Task::get_ShouldNotifyDebuggerOfWaitCompletion()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Task_get_ShouldNotifyDebuggerOfWaitCompletion_m7AB76993855A98D7FA90003FBACF579C7D7E4534 (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* __this, const RuntimeMethod* method) ;
// System.Boolean System.Threading.Tasks.Task::AnyTaskRequiresNotifyDebuggerOfWaitCompletion(System.Threading.Tasks.Task[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Task_AnyTaskRequiresNotifyDebuggerOfWaitCompletion_m90C692B044AB99E76F7BF6A2BCD909DCDD01FBA3 (TaskU5BU5D_t368E447BD9A179BA9A26BAAABF1BAE9CA79F60B3* ___tasks0, const RuntimeMethod* method) ;
// System.Void System.Threading.Tasks.Task`1<System.Object[]>::.ctor()
inline void Task_1__ctor_m03A743E024D677111A87084460EE475C07C47C83 (Task_1_t5B1ACE26BA285FAD19DF6E5F61190F03850816CB* __this, const RuntimeMethod* method)
{
	((  void (*) (Task_1_t5B1ACE26BA285FAD19DF6E5F61190F03850816CB*, const RuntimeMethod*))Task_1__ctor_m2A3A26E17D6BE69CEEC048C0599F76FE4C6D24A0_gshared)(__this, method);
}
// System.Void System.Threading.Tasks.Task/WhenAllPromise`1<System.Object>::Invoke(System.Threading.Tasks.Task)
inline void WhenAllPromise_1_Invoke_mC22438FD090403404288BA997992DE80EE2FA153 (WhenAllPromise_1_t6B7D3ABF753EA8F6F7581C787A300C50CBA54FF0* __this, Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* ___ignored0, const RuntimeMethod* method)
{
	((  void (*) (WhenAllPromise_1_t6B7D3ABF753EA8F6F7581C787A300C50CBA54FF0*, Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*, const RuntimeMethod*))WhenAllPromise_1_Invoke_mC22438FD090403404288BA997992DE80EE2FA153_gshared)(__this, ___ignored0, method);
}
// TResult System.Threading.Tasks.Task`1<System.Object>::GetResultCore(System.Boolean)
inline RuntimeObject* Task_1_GetResultCore_mA6141C79F2DD78034AE5BC0311D4D143A5C320B9 (Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2* __this, bool ___waitCompletionNotification0, const RuntimeMethod* method)
{
	return ((  RuntimeObject* (*) (Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2*, bool, const RuntimeMethod*))Task_1_GetResultCore_mA6141C79F2DD78034AE5BC0311D4D143A5C320B9_gshared)(__this, ___waitCompletionNotification0, method);
}
// System.Boolean System.Threading.Tasks.Task`1<System.Object[]>::TrySetResult(TResult)
inline bool Task_1_TrySetResult_mAF6B2C87E8E79DC5099DFAD16F0FC5AB4AA2D1E4 (Task_1_t5B1ACE26BA285FAD19DF6E5F61190F03850816CB* __this, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___result0, const RuntimeMethod* method)
{
	return ((  bool (*) (Task_1_t5B1ACE26BA285FAD19DF6E5F61190F03850816CB*, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*, const RuntimeMethod*))Task_1_TrySetResult_m2EE766FD3F76F4824990F4A93ED1F7253ECE014C_gshared)(__this, ___result0, method);
}
// System.Void System.Linq.Enumerable/Iterator`1<System.Object>::.ctor()
inline void Iterator_1__ctor_m3E47867714E05673E54C6B73D9242F5FFADA1F63 (Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA* __this, const RuntimeMethod* method)
{
	((  void (*) (Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*, const RuntimeMethod*))Iterator_1__ctor_m3E47867714E05673E54C6B73D9242F5FFADA1F63_gshared)(__this, method);
}
// System.Void System.Linq.Enumerable/WhereArrayIterator`1<System.Object>::.ctor(TSource[],System.Func`2<TSource,System.Boolean>)
inline void WhereArrayIterator_1__ctor_mC17BAA23BA92C7455512FDA5B1618C6D2B54ACA2 (WhereArrayIterator_1_t027D2511F9B69346688FE3E5623EF2BEE81E9FAA* __this, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___source0, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate1, const RuntimeMethod* method)
{
	((  void (*) (WhereArrayIterator_1_t027D2511F9B69346688FE3E5623EF2BEE81E9FAA*, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*, const RuntimeMethod*))WhereArrayIterator_1__ctor_mC17BAA23BA92C7455512FDA5B1618C6D2B54ACA2_gshared)(__this, ___source0, ___predicate1, method);
}
// TResult System.Func`2<System.Object,System.Boolean>::Invoke(T)
inline bool Func_2_Invoke_m2014423FB900F135C8FF994125604FF9E6AAE829_inline (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* __this, RuntimeObject* ___arg0, const RuntimeMethod* method)
{
	return ((  bool (*) (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*, RuntimeObject*, const RuntimeMethod*))Func_2_Invoke_m2014423FB900F135C8FF994125604FF9E6AAE829_gshared_inline)(__this, ___arg0, method);
}
// System.Func`2<TSource,System.Boolean> System.Linq.Enumerable::CombinePredicates<System.Object>(System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,System.Boolean>)
inline Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* Enumerable_CombinePredicates_TisRuntimeObject_m613479C29B013E8FC2987E22F42A3BC6CC2C9768 (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate10, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate21, const RuntimeMethod* method)
{
	return ((  Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* (*) (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*, const RuntimeMethod*))Enumerable_CombinePredicates_TisRuntimeObject_m613479C29B013E8FC2987E22F42A3BC6CC2C9768_gshared)(___predicate10, ___predicate21, method);
}
// System.Void System.Linq.Enumerable/Iterator`1<System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::.ctor()
inline void Iterator_1__ctor_m27CEA132FC2E7D7BA6A8529CE0647F2709FC29A3 (Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40* __this, const RuntimeMethod* method)
{
	((  void (*) (Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*, const RuntimeMethod*))Iterator_1__ctor_m27CEA132FC2E7D7BA6A8529CE0647F2709FC29A3_gshared)(__this, method);
}
// System.Void System.Linq.Enumerable/WhereEnumerableIterator`1<System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::.ctor(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>)
inline void WhereEnumerableIterator_1__ctor_mC87BD639674AE32961C0AC5A5878F4144E7DA780 (WhereEnumerableIterator_1_tB576B4EE76F70B81DDF11FA35260EC589825E7C5* __this, RuntimeObject* ___source0, Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B* ___predicate1, const RuntimeMethod* method)
{
	((  void (*) (WhereEnumerableIterator_1_tB576B4EE76F70B81DDF11FA35260EC589825E7C5*, RuntimeObject*, Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B*, const RuntimeMethod*))WhereEnumerableIterator_1__ctor_mC87BD639674AE32961C0AC5A5878F4144E7DA780_gshared)(__this, ___source0, ___predicate1, method);
}
// System.Void System.Linq.Enumerable/Iterator`1<System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::Dispose()
inline void Iterator_1_Dispose_m51BD0D9CCB4CB032C42DA20F3C867462771689AB (Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40* __this, const RuntimeMethod* method)
{
	((  void (*) (Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*, const RuntimeMethod*))Iterator_1_Dispose_m51BD0D9CCB4CB032C42DA20F3C867462771689AB_gshared)(__this, method);
}
// TResult System.Func`2<System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>,System.Boolean>::Invoke(T)
inline bool Func_2_Invoke_m1DC9E7C418A082CE71CE5A7F12B8F8F30528804F_inline (Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B* __this, ValueTuple_2_t761B36235D5F97F497A1CBCD077DF3858814FADE ___arg0, const RuntimeMethod* method)
{
	return ((  bool (*) (Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B*, ValueTuple_2_t761B36235D5F97F497A1CBCD077DF3858814FADE, const RuntimeMethod*))Func_2_Invoke_m1DC9E7C418A082CE71CE5A7F12B8F8F30528804F_gshared_inline)(__this, ___arg0, method);
}
// System.Func`2<TSource,System.Boolean> System.Linq.Enumerable::CombinePredicates<System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>(System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,System.Boolean>)
inline Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B* Enumerable_CombinePredicates_TisValueTuple_2_t761B36235D5F97F497A1CBCD077DF3858814FADE_m97FA1ECA0B2F0FA21E3CF45AAEB7C5157CC47603 (Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B* ___predicate10, Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B* ___predicate21, const RuntimeMethod* method)
{
	return ((  Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B* (*) (Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B*, Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B*, const RuntimeMethod*))Enumerable_CombinePredicates_TisValueTuple_2_t761B36235D5F97F497A1CBCD077DF3858814FADE_m97FA1ECA0B2F0FA21E3CF45AAEB7C5157CC47603_gshared)(___predicate10, ___predicate21, method);
}
// System.Void System.Linq.Enumerable/WhereEnumerableIterator`1<System.Object>::.ctor(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>)
inline void WhereEnumerableIterator_1__ctor_m8C0DA4CDA5431C03561F67C4393BB18CDD891F01 (WhereEnumerableIterator_1_t1E787D13759F5A31C94B3FAED181402B25C278F4* __this, RuntimeObject* ___source0, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate1, const RuntimeMethod* method)
{
	((  void (*) (WhereEnumerableIterator_1_t1E787D13759F5A31C94B3FAED181402B25C278F4*, RuntimeObject*, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*, const RuntimeMethod*))WhereEnumerableIterator_1__ctor_m8C0DA4CDA5431C03561F67C4393BB18CDD891F01_gshared)(__this, ___source0, ___predicate1, method);
}
// System.Void System.Linq.Enumerable/Iterator`1<System.Object>::Dispose()
inline void Iterator_1_Dispose_m953BCF886C8A63456821023DBA45EBD9AC44EB07 (Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA* __this, const RuntimeMethod* method)
{
	((  void (*) (Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*, const RuntimeMethod*))Iterator_1_Dispose_m953BCF886C8A63456821023DBA45EBD9AC44EB07_gshared)(__this, method);
}
// System.Void System.Linq.Enumerable/Iterator`1<System.Single>::.ctor()
inline void Iterator_1__ctor_m7FEBAE985CDE5DD1CB255EB4A3D65F05AA8D2AEF (Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E* __this, const RuntimeMethod* method)
{
	((  void (*) (Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*, const RuntimeMethod*))Iterator_1__ctor_m7FEBAE985CDE5DD1CB255EB4A3D65F05AA8D2AEF_gshared)(__this, method);
}
// System.Void System.Linq.Enumerable/WhereEnumerableIterator`1<System.Single>::.ctor(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>)
inline void WhereEnumerableIterator_1__ctor_mCB49A03958EF827EF6CE47402259941EAB31D984 (WhereEnumerableIterator_1_t7BB2D1D9F8A6E52243A87F45DFEEA4209D685F7C* __this, RuntimeObject* ___source0, Func_2_t49E998685259ADE759F9329BF66F20DE8667006E* ___predicate1, const RuntimeMethod* method)
{
	((  void (*) (WhereEnumerableIterator_1_t7BB2D1D9F8A6E52243A87F45DFEEA4209D685F7C*, RuntimeObject*, Func_2_t49E998685259ADE759F9329BF66F20DE8667006E*, const RuntimeMethod*))WhereEnumerableIterator_1__ctor_mCB49A03958EF827EF6CE47402259941EAB31D984_gshared)(__this, ___source0, ___predicate1, method);
}
// System.Void System.Linq.Enumerable/Iterator`1<System.Single>::Dispose()
inline void Iterator_1_Dispose_mC0A88E7EEED5A9CFDCACF160DE8F58A713DFF222 (Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E* __this, const RuntimeMethod* method)
{
	((  void (*) (Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*, const RuntimeMethod*))Iterator_1_Dispose_mC0A88E7EEED5A9CFDCACF160DE8F58A713DFF222_gshared)(__this, method);
}
// TResult System.Func`2<System.Single,System.Boolean>::Invoke(T)
inline bool Func_2_Invoke_m1FE6F2A4EC23CC595897C55AE7B0BDA8969044D7_inline (Func_2_t49E998685259ADE759F9329BF66F20DE8667006E* __this, float ___arg0, const RuntimeMethod* method)
{
	return ((  bool (*) (Func_2_t49E998685259ADE759F9329BF66F20DE8667006E*, float, const RuntimeMethod*))Func_2_Invoke_m1FE6F2A4EC23CC595897C55AE7B0BDA8969044D7_gshared_inline)(__this, ___arg0, method);
}
// System.Func`2<TSource,System.Boolean> System.Linq.Enumerable::CombinePredicates<System.Single>(System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,System.Boolean>)
inline Func_2_t49E998685259ADE759F9329BF66F20DE8667006E* Enumerable_CombinePredicates_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_m2F9BC4BEE0498F9B3486BA254B507C1FCE89A763 (Func_2_t49E998685259ADE759F9329BF66F20DE8667006E* ___predicate10, Func_2_t49E998685259ADE759F9329BF66F20DE8667006E* ___predicate21, const RuntimeMethod* method)
{
	return ((  Func_2_t49E998685259ADE759F9329BF66F20DE8667006E* (*) (Func_2_t49E998685259ADE759F9329BF66F20DE8667006E*, Func_2_t49E998685259ADE759F9329BF66F20DE8667006E*, const RuntimeMethod*))Enumerable_CombinePredicates_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_m2F9BC4BEE0498F9B3486BA254B507C1FCE89A763_gshared)(___predicate10, ___predicate21, method);
}
// System.Void System.Linq.Enumerable/WhereListIterator`1<System.Object>::.ctor(System.Collections.Generic.List`1<TSource>,System.Func`2<TSource,System.Boolean>)
inline void WhereListIterator_1__ctor_mEF8F62B9078E538C1DC46BCB876C2AC5B29EA73B (WhereListIterator_1_t1F40F08BAF8586F2C09294085BC605CC2FA432EB* __this, List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* ___source0, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate1, const RuntimeMethod* method)
{
	((  void (*) (WhereListIterator_1_t1F40F08BAF8586F2C09294085BC605CC2FA432EB*, List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D*, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*, const RuntimeMethod*))WhereListIterator_1__ctor_mEF8F62B9078E538C1DC46BCB876C2AC5B29EA73B_gshared)(__this, ___source0, ___predicate1, method);
}
// System.Collections.Generic.List`1/Enumerator<T> System.Collections.Generic.List`1<System.Object>::GetEnumerator()
inline Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A List_1_GetEnumerator_mD8294A7FA2BEB1929487127D476F8EC1CDC23BFC (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method)
{
	return ((  Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A (*) (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D*, const RuntimeMethod*))List_1_GetEnumerator_mD8294A7FA2BEB1929487127D476F8EC1CDC23BFC_gshared)(__this, method);
}
// T System.Collections.Generic.List`1/Enumerator<System.Object>::get_Current()
inline RuntimeObject* Enumerator_get_Current_m6330F15D18EE4F547C05DF9BF83C5EB710376027_inline (Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A* __this, const RuntimeMethod* method)
{
	return ((  RuntimeObject* (*) (Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A*, const RuntimeMethod*))Enumerator_get_Current_m6330F15D18EE4F547C05DF9BF83C5EB710376027_gshared_inline)(__this, method);
}
// System.Boolean System.Collections.Generic.List`1/Enumerator<System.Object>::MoveNext()
inline bool Enumerator_MoveNext_mE921CC8F29FBBDE7CC3209A0ED0D921D58D00BCB (Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A*, const RuntimeMethod*))Enumerator_MoveNext_mE921CC8F29FBBDE7CC3209A0ED0D921D58D00BCB_gshared)(__this, method);
}
// System.Void System.Linq.Enumerable/WhereSelectArrayIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::.ctor(TSource[],System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
inline void WhereSelectArrayIterator_2__ctor_m5DCBBB86A9209D1F91CCB6D656EDCD6A2AF93A31 (WhereSelectArrayIterator_2_t30000EF5692CE8B73879E35CB9EA48B4DCF6EAB5* __this, ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46* ___source0, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate1, Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B* ___selector2, const RuntimeMethod* method)
{
	((  void (*) (WhereSelectArrayIterator_2_t30000EF5692CE8B73879E35CB9EA48B4DCF6EAB5*, ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46*, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*, Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B*, const RuntimeMethod*))WhereSelectArrayIterator_2__ctor_m5DCBBB86A9209D1F91CCB6D656EDCD6A2AF93A31_gshared)(__this, ___source0, ___predicate1, ___selector2, method);
}
// TResult System.Func`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Boolean>::Invoke(T)
inline bool Func_2_Invoke_m12B4F5C7D4C30A85BD18305B0CDE8DF42312F402_inline (Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* __this, ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 ___arg0, const RuntimeMethod* method)
{
	return ((  bool (*) (Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*, ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48, const RuntimeMethod*))Func_2_Invoke_m12B4F5C7D4C30A85BD18305B0CDE8DF42312F402_gshared_inline)(__this, ___arg0, method);
}
// TResult System.Func`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::Invoke(T)
inline ValueTuple_2_t761B36235D5F97F497A1CBCD077DF3858814FADE Func_2_Invoke_m3794F6EAED760CA17FC7A24439DC423AC6BF8FB9_inline (Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B* __this, ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 ___arg0, const RuntimeMethod* method)
{
	return ((  ValueTuple_2_t761B36235D5F97F497A1CBCD077DF3858814FADE (*) (Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B*, ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48, const RuntimeMethod*))Func_2_Invoke_m3794F6EAED760CA17FC7A24439DC423AC6BF8FB9_gshared_inline)(__this, ___arg0, method);
}
// System.Void System.Linq.Enumerable/WhereSelectArrayIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Object>::.ctor(TSource[],System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
inline void WhereSelectArrayIterator_2__ctor_m7932AB67118B332F6FAEB44AA075721F4C0BC26B (WhereSelectArrayIterator_2_tBABE519E8262C3B19F12FA57F0EBBEF62306E2EE* __this, ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46* ___source0, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate1, Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287* ___selector2, const RuntimeMethod* method)
{
	((  void (*) (WhereSelectArrayIterator_2_tBABE519E8262C3B19F12FA57F0EBBEF62306E2EE*, ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46*, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*, Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287*, const RuntimeMethod*))WhereSelectArrayIterator_2__ctor_m7932AB67118B332F6FAEB44AA075721F4C0BC26B_gshared)(__this, ___source0, ___predicate1, ___selector2, method);
}
// TResult System.Func`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Object>::Invoke(T)
inline RuntimeObject* Func_2_Invoke_m9EB50B06293708FB57A5A30A460483868FFF99D0_inline (Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287* __this, ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 ___arg0, const RuntimeMethod* method)
{
	return ((  RuntimeObject* (*) (Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287*, ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48, const RuntimeMethod*))Func_2_Invoke_m9EB50B06293708FB57A5A30A460483868FFF99D0_gshared_inline)(__this, ___arg0, method);
}
// System.Void System.Linq.Enumerable/WhereSelectArrayIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Single>::.ctor(TSource[],System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
inline void WhereSelectArrayIterator_2__ctor_mA507C5E2A96C465B089EC01D8C9D02D9D5D3D0E4 (WhereSelectArrayIterator_2_tFF39CC0F264CA7C80FEA868C00FD12A1D45DC58F* __this, ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46* ___source0, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate1, Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72* ___selector2, const RuntimeMethod* method)
{
	((  void (*) (WhereSelectArrayIterator_2_tFF39CC0F264CA7C80FEA868C00FD12A1D45DC58F*, ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46*, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*, Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72*, const RuntimeMethod*))WhereSelectArrayIterator_2__ctor_mA507C5E2A96C465B089EC01D8C9D02D9D5D3D0E4_gshared)(__this, ___source0, ___predicate1, ___selector2, method);
}
// TResult System.Func`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Single>::Invoke(T)
inline float Func_2_Invoke_mC888CBA13A68639AC714989A4F3DC91C432BB307_inline (Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72* __this, ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 ___arg0, const RuntimeMethod* method)
{
	return ((  float (*) (Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72*, ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48, const RuntimeMethod*))Func_2_Invoke_mC888CBA13A68639AC714989A4F3DC91C432BB307_gshared_inline)(__this, ___arg0, method);
}
// System.Void System.Linq.Enumerable/WhereSelectArrayIterator`2<System.Object,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::.ctor(TSource[],System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
inline void WhereSelectArrayIterator_2__ctor_m542DF3BD7D0591BD1A6CF2AC84E55CB5431BBDD7 (WhereSelectArrayIterator_2_tB6BDCFF0C2C4DEEFA9D69A00383291FAA9B56E24* __this, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___source0, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate1, Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9* ___selector2, const RuntimeMethod* method)
{
	((  void (*) (WhereSelectArrayIterator_2_tB6BDCFF0C2C4DEEFA9D69A00383291FAA9B56E24*, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*, Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9*, const RuntimeMethod*))WhereSelectArrayIterator_2__ctor_m542DF3BD7D0591BD1A6CF2AC84E55CB5431BBDD7_gshared)(__this, ___source0, ___predicate1, ___selector2, method);
}
// TResult System.Func`2<System.Object,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::Invoke(T)
inline ValueTuple_2_t761B36235D5F97F497A1CBCD077DF3858814FADE Func_2_Invoke_m30EE337C21F1CA674046A64A8A2DA68A02701634_inline (Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9* __this, RuntimeObject* ___arg0, const RuntimeMethod* method)
{
	return ((  ValueTuple_2_t761B36235D5F97F497A1CBCD077DF3858814FADE (*) (Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9*, RuntimeObject*, const RuntimeMethod*))Func_2_Invoke_m30EE337C21F1CA674046A64A8A2DA68A02701634_gshared_inline)(__this, ___arg0, method);
}
// System.Void System.Linq.Enumerable/WhereSelectArrayIterator`2<System.Object,System.Single>::.ctor(TSource[],System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
inline void WhereSelectArrayIterator_2__ctor_m98F1E0E34745862A883CE6A1CCE4DE42E16CF8A9 (WhereSelectArrayIterator_2_tD87299FD8280F38D5E50B5174574F2F20188E0D4* __this, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___source0, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate1, Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12* ___selector2, const RuntimeMethod* method)
{
	((  void (*) (WhereSelectArrayIterator_2_tD87299FD8280F38D5E50B5174574F2F20188E0D4*, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*, Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12*, const RuntimeMethod*))WhereSelectArrayIterator_2__ctor_m98F1E0E34745862A883CE6A1CCE4DE42E16CF8A9_gshared)(__this, ___source0, ___predicate1, ___selector2, method);
}
// TResult System.Func`2<System.Object,System.Single>::Invoke(T)
inline float Func_2_Invoke_mBE16A5FDA5E80CCBA51D69334EF21C0F03D353AF_inline (Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12* __this, RuntimeObject* ___arg0, const RuntimeMethod* method)
{
	return ((  float (*) (Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12*, RuntimeObject*, const RuntimeMethod*))Func_2_Invoke_mBE16A5FDA5E80CCBA51D69334EF21C0F03D353AF_gshared_inline)(__this, ___arg0, method);
}
// System.Void System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::.ctor(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
inline void WhereSelectEnumerableIterator_2__ctor_mA080BFD551E780DEE7C4C1CFE7BAAA6662478900 (WhereSelectEnumerableIterator_2_t4A082C2E79F346CC9DA87508903869069C5C6454* __this, RuntimeObject* ___source0, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate1, Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B* ___selector2, const RuntimeMethod* method)
{
	((  void (*) (WhereSelectEnumerableIterator_2_t4A082C2E79F346CC9DA87508903869069C5C6454*, RuntimeObject*, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*, Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B*, const RuntimeMethod*))WhereSelectEnumerableIterator_2__ctor_mA080BFD551E780DEE7C4C1CFE7BAAA6662478900_gshared)(__this, ___source0, ___predicate1, ___selector2, method);
}
// System.Void System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Object>::.ctor(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
inline void WhereSelectEnumerableIterator_2__ctor_mA9895FAF25F9C9A7894DD268E375875F8FBA6E76 (WhereSelectEnumerableIterator_2_t27953926A9605639C866748EA3EF6801E63B5430* __this, RuntimeObject* ___source0, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate1, Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287* ___selector2, const RuntimeMethod* method)
{
	((  void (*) (WhereSelectEnumerableIterator_2_t27953926A9605639C866748EA3EF6801E63B5430*, RuntimeObject*, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*, Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287*, const RuntimeMethod*))WhereSelectEnumerableIterator_2__ctor_mA9895FAF25F9C9A7894DD268E375875F8FBA6E76_gshared)(__this, ___source0, ___predicate1, ___selector2, method);
}
// System.Void System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Single>::.ctor(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
inline void WhereSelectEnumerableIterator_2__ctor_m00D2CEA2D2B5A0C3A03E1A7A7F9BBA5229DEF1B2 (WhereSelectEnumerableIterator_2_tA24C2F75772C5AB98B395DC9C70CDD82327A82C4* __this, RuntimeObject* ___source0, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate1, Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72* ___selector2, const RuntimeMethod* method)
{
	((  void (*) (WhereSelectEnumerableIterator_2_tA24C2F75772C5AB98B395DC9C70CDD82327A82C4*, RuntimeObject*, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*, Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72*, const RuntimeMethod*))WhereSelectEnumerableIterator_2__ctor_m00D2CEA2D2B5A0C3A03E1A7A7F9BBA5229DEF1B2_gshared)(__this, ___source0, ___predicate1, ___selector2, method);
}
// System.Void System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.Object,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::.ctor(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
inline void WhereSelectEnumerableIterator_2__ctor_m5538389BC390D50DBDA10D3C0945A79D830641B3 (WhereSelectEnumerableIterator_2_t2379CEC793B1983191566FBE7853007579556057* __this, RuntimeObject* ___source0, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate1, Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9* ___selector2, const RuntimeMethod* method)
{
	((  void (*) (WhereSelectEnumerableIterator_2_t2379CEC793B1983191566FBE7853007579556057*, RuntimeObject*, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*, Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9*, const RuntimeMethod*))WhereSelectEnumerableIterator_2__ctor_m5538389BC390D50DBDA10D3C0945A79D830641B3_gshared)(__this, ___source0, ___predicate1, ___selector2, method);
}
// System.Void System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.Object,System.Single>::.ctor(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
inline void WhereSelectEnumerableIterator_2__ctor_mE8B29F90A4CD398E256C48F9451231F2EE02008E (WhereSelectEnumerableIterator_2_tF7B17AE20C591499CCC69CF4CFEA521F28D66E94* __this, RuntimeObject* ___source0, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate1, Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12* ___selector2, const RuntimeMethod* method)
{
	((  void (*) (WhereSelectEnumerableIterator_2_tF7B17AE20C591499CCC69CF4CFEA521F28D66E94*, RuntimeObject*, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*, Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12*, const RuntimeMethod*))WhereSelectEnumerableIterator_2__ctor_mE8B29F90A4CD398E256C48F9451231F2EE02008E_gshared)(__this, ___source0, ___predicate1, ___selector2, method);
}
// System.Void System.Linq.Enumerable/WhereSelectListIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::.ctor(System.Collections.Generic.List`1<TSource>,System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
inline void WhereSelectListIterator_2__ctor_m8AEB85EB9B265A2F69C0BE789DA26BA006C9E903 (WhereSelectListIterator_2_t8F14DC7F95888AA905CF6AE27A6AFC1205DC2933* __this, List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE* ___source0, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate1, Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B* ___selector2, const RuntimeMethod* method)
{
	((  void (*) (WhereSelectListIterator_2_t8F14DC7F95888AA905CF6AE27A6AFC1205DC2933*, List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE*, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*, Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B*, const RuntimeMethod*))WhereSelectListIterator_2__ctor_m8AEB85EB9B265A2F69C0BE789DA26BA006C9E903_gshared)(__this, ___source0, ___predicate1, ___selector2, method);
}
// System.Collections.Generic.List`1/Enumerator<T> System.Collections.Generic.List`1<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>>::GetEnumerator()
inline Enumerator_t3ACCE1C7605054D2EF26F63B987FBD7CEA380901 List_1_GetEnumerator_m180F98F72F5DA91BF8696E010117148062025D82 (List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE* __this, const RuntimeMethod* method)
{
	return ((  Enumerator_t3ACCE1C7605054D2EF26F63B987FBD7CEA380901 (*) (List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE*, const RuntimeMethod*))List_1_GetEnumerator_m180F98F72F5DA91BF8696E010117148062025D82_gshared)(__this, method);
}
// T System.Collections.Generic.List`1/Enumerator<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>>::get_Current()
inline ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 Enumerator_get_Current_mE43C1A4AD61C4A5D58C7E5F6D98CD7DE40A64987_inline (Enumerator_t3ACCE1C7605054D2EF26F63B987FBD7CEA380901* __this, const RuntimeMethod* method)
{
	return ((  ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 (*) (Enumerator_t3ACCE1C7605054D2EF26F63B987FBD7CEA380901*, const RuntimeMethod*))Enumerator_get_Current_mE43C1A4AD61C4A5D58C7E5F6D98CD7DE40A64987_gshared_inline)(__this, method);
}
// System.Boolean System.Collections.Generic.List`1/Enumerator<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>>::MoveNext()
inline bool Enumerator_MoveNext_mD0B6A0F10BA08A58F39285C8C8309111A79A3F7A (Enumerator_t3ACCE1C7605054D2EF26F63B987FBD7CEA380901* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Enumerator_t3ACCE1C7605054D2EF26F63B987FBD7CEA380901*, const RuntimeMethod*))Enumerator_MoveNext_mD0B6A0F10BA08A58F39285C8C8309111A79A3F7A_gshared)(__this, method);
}
// System.Void System.Linq.Enumerable/WhereSelectListIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Object>::.ctor(System.Collections.Generic.List`1<TSource>,System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
inline void WhereSelectListIterator_2__ctor_mB2F43B2048C674205F65D38CD3AD6FE74C08FBE0 (WhereSelectListIterator_2_tEB04EFC71ED85DC1E14F1FC388A8CCAF2FB25E8B* __this, List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE* ___source0, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate1, Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287* ___selector2, const RuntimeMethod* method)
{
	((  void (*) (WhereSelectListIterator_2_tEB04EFC71ED85DC1E14F1FC388A8CCAF2FB25E8B*, List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE*, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*, Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287*, const RuntimeMethod*))WhereSelectListIterator_2__ctor_mB2F43B2048C674205F65D38CD3AD6FE74C08FBE0_gshared)(__this, ___source0, ___predicate1, ___selector2, method);
}
// System.Void System.Linq.Enumerable/WhereSelectListIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Single>::.ctor(System.Collections.Generic.List`1<TSource>,System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
inline void WhereSelectListIterator_2__ctor_m0889BA82694536AAAE4B2C793DC8825399744C3D (WhereSelectListIterator_2_t87543D52D098CB5DBA7AF3A904A5A1183466C3C6* __this, List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE* ___source0, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate1, Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72* ___selector2, const RuntimeMethod* method)
{
	((  void (*) (WhereSelectListIterator_2_t87543D52D098CB5DBA7AF3A904A5A1183466C3C6*, List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE*, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*, Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72*, const RuntimeMethod*))WhereSelectListIterator_2__ctor_m0889BA82694536AAAE4B2C793DC8825399744C3D_gshared)(__this, ___source0, ___predicate1, ___selector2, method);
}
// System.Void System.Linq.Enumerable/WhereSelectListIterator`2<System.Object,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::.ctor(System.Collections.Generic.List`1<TSource>,System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
inline void WhereSelectListIterator_2__ctor_m6F364DC8CF50F39787083DBD5334AC40433CC8B8 (WhereSelectListIterator_2_t6D877164A02A148F4B5F654B133D07C23FA7904A* __this, List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* ___source0, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate1, Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9* ___selector2, const RuntimeMethod* method)
{
	((  void (*) (WhereSelectListIterator_2_t6D877164A02A148F4B5F654B133D07C23FA7904A*, List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D*, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*, Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9*, const RuntimeMethod*))WhereSelectListIterator_2__ctor_m6F364DC8CF50F39787083DBD5334AC40433CC8B8_gshared)(__this, ___source0, ___predicate1, ___selector2, method);
}
// System.Void System.Linq.Enumerable/WhereSelectListIterator`2<System.Object,System.Single>::.ctor(System.Collections.Generic.List`1<TSource>,System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
inline void WhereSelectListIterator_2__ctor_m22BAFF678F9378729DD9E5551BBA6BA69FDEEF94 (WhereSelectListIterator_2_tFE32164179EE37B666D799CFB7F5167FB4C464D9* __this, List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* ___source0, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate1, Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12* ___selector2, const RuntimeMethod* method)
{
	((  void (*) (WhereSelectListIterator_2_tFE32164179EE37B666D799CFB7F5167FB4C464D9*, List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D*, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*, Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12*, const RuntimeMethod*))WhereSelectListIterator_2__ctor_m22BAFF678F9378729DD9E5551BBA6BA69FDEEF94_gshared)(__this, ___source0, ___predicate1, ___selector2, method);
}
// System.Void System.Threading.Tasks.DebuggerSupport::AddToActiveTasksNonInlined(System.Threading.Tasks.Task)
IL2CPP_EXTERN_C IL2CPP_NO_INLINE IL2CPP_METHOD_ATTR void DebuggerSupport_AddToActiveTasksNonInlined_m28785ADBB0443B6CCDC12C4A7DA297F1FFAEF889 (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* ___task0, const RuntimeMethod* method) ;
// System.Void System.Threading.Tasks.DebuggerSupport::RemoveFromActiveTasksNonInlined(System.Threading.Tasks.Task)
IL2CPP_EXTERN_C IL2CPP_NO_INLINE IL2CPP_METHOD_ATTR void DebuggerSupport_RemoveFromActiveTasksNonInlined_m328F6FE7F9066EAD92528B6AD6BB08B12BA9A2C0 (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* ___task0, const RuntimeMethod* method) ;
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void System.WeakReference`1<System.Object>::.ctor(T)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WeakReference_1__ctor_m932665C8861A22B177DC1ACF1EDAA87E1624B5AC_gshared (WeakReference_1_tED795563AD26F795CED3BBCD488AB1694E385BCE* __this, RuntimeObject* ___target0, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = ___target0;
		WeakReference_1__ctor_m99141AB321E022D9933448CDD7139BC9FAA443E8(__this, L_0, (bool)0, il2cpp_rgctx_method(method->klass->rgctx_data, 1));
		return;
	}
}
// System.Void System.WeakReference`1<System.Object>::.ctor(T,System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WeakReference_1__ctor_m99141AB321E022D9933448CDD7139BC9FAA443E8_gshared (WeakReference_1_tED795563AD26F795CED3BBCD488AB1694E385BCE* __this, RuntimeObject* ___target0, bool ___trackResurrection1, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	int32_t G_B3_0 = 0;
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2((RuntimeObject*)__this, NULL);
		bool L_0 = ___trackResurrection1;
		__this->___trackResurrection_1 = L_0;
		bool L_1 = ___trackResurrection1;
		if (L_1)
		{
			goto IL_0013;
		}
	}
	{
		G_B3_0 = 0;
		goto IL_0014;
	}

IL_0013:
	{
		G_B3_0 = 1;
	}

IL_0014:
	{
		V_0 = (int32_t)G_B3_0;
		RuntimeObject* L_2 = ___target0;
		int32_t L_3 = V_0;
		GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC L_4;
		L_4 = GCHandle_Alloc_m3BFD398427352FC756FFE078F01A504B681352EC(L_2, L_3, NULL);
		__this->___handle_0 = L_4;
		return;
	}
}
// System.Void System.WeakReference`1<System.Object>::.ctor(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WeakReference_1__ctor_m2289DC7F3597E1BA77555086A86F91807FDC96E2_gshared (WeakReference_1_tED795563AD26F795CED3BBCD488AB1694E385BCE* __this, SerializationInfo_t3C47F63E24BEB9FCE2DC6309E027F238DC5C5E37* ___info0, StreamingContext_t56760522A751890146EE45F82F866B55B7E33677 ___context1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Type_t_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral5CA6E7C0AE72196B2817D93A78C719652EC691C0);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral7D20B8219CA0491872B2E811B262066A5DD875A7);
		s_Il2CppMethodInitialized = true;
	}
	RuntimeObject* V_0 = NULL;
	int32_t V_1 = 0;
	int32_t G_B5_0 = 0;
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2((RuntimeObject*)__this, NULL);
		SerializationInfo_t3C47F63E24BEB9FCE2DC6309E027F238DC5C5E37* L_0 = ___info0;
		if (L_0)
		{
			goto IL_0014;
		}
	}
	{
		ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129* L_1 = (ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129_il2cpp_TypeInfo_var)));
		NullCheck(L_1);
		ArgumentNullException__ctor_m444AE141157E333844FC1A9500224C2F9FD24F4B(L_1, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralA7B00F7F25C375B2501A6ADBC86D092B23977085)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_1, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&WeakReference_1__ctor_m2289DC7F3597E1BA77555086A86F91807FDC96E2_RuntimeMethod_var)));
	}

IL_0014:
	{
		SerializationInfo_t3C47F63E24BEB9FCE2DC6309E027F238DC5C5E37* L_2 = ___info0;
		NullCheck(L_2);
		bool L_3;
		L_3 = SerializationInfo_GetBoolean_m8335F8E11B572AB6B5BF85A9355D6888D5847EF5(L_2, _stringLiteral7D20B8219CA0491872B2E811B262066A5DD875A7, NULL);
		__this->___trackResurrection_1 = L_3;
		SerializationInfo_t3C47F63E24BEB9FCE2DC6309E027F238DC5C5E37* L_4 = ___info0;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_5 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->klass->rgctx_data, 3)) };
		il2cpp_codegen_runtime_class_init_inline(Type_t_il2cpp_TypeInfo_var);
		Type_t* L_6;
		L_6 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_5, NULL);
		NullCheck(L_4);
		RuntimeObject* L_7;
		L_7 = SerializationInfo_GetValue_mE6091C2E906E113455D05E734C86F43B8E1D1034(L_4, _stringLiteral5CA6E7C0AE72196B2817D93A78C719652EC691C0, L_6, NULL);
		V_0 = L_7;
		bool L_8 = (bool)__this->___trackResurrection_1;
		if (L_8)
		{
			goto IL_0046;
		}
	}
	{
		G_B5_0 = 0;
		goto IL_0047;
	}

IL_0046:
	{
		G_B5_0 = 1;
	}

IL_0047:
	{
		V_1 = (int32_t)G_B5_0;
		RuntimeObject* L_9 = V_0;
		int32_t L_10 = V_1;
		GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC L_11;
		L_11 = GCHandle_Alloc_m3BFD398427352FC756FFE078F01A504B681352EC(L_9, L_10, NULL);
		__this->___handle_0 = L_11;
		return;
	}
}
// System.Void System.WeakReference`1<System.Object>::GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WeakReference_1_GetObjectData_m6F2E12AF126FAE536995F52F9501498BDA5917A7_gshared (WeakReference_1_tED795563AD26F795CED3BBCD488AB1694E385BCE* __this, SerializationInfo_t3C47F63E24BEB9FCE2DC6309E027F238DC5C5E37* ___info0, StreamingContext_t56760522A751890146EE45F82F866B55B7E33677 ___context1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral5CA6E7C0AE72196B2817D93A78C719652EC691C0);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral7D20B8219CA0491872B2E811B262066A5DD875A7);
		s_Il2CppMethodInitialized = true;
	}
	{
		SerializationInfo_t3C47F63E24BEB9FCE2DC6309E027F238DC5C5E37* L_0 = ___info0;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129* L_1 = (ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129_il2cpp_TypeInfo_var)));
		NullCheck(L_1);
		ArgumentNullException__ctor_m444AE141157E333844FC1A9500224C2F9FD24F4B(L_1, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralA7B00F7F25C375B2501A6ADBC86D092B23977085)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_1, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&WeakReference_1_GetObjectData_m6F2E12AF126FAE536995F52F9501498BDA5917A7_RuntimeMethod_var)));
	}

IL_000e:
	{
		SerializationInfo_t3C47F63E24BEB9FCE2DC6309E027F238DC5C5E37* L_2 = ___info0;
		bool L_3 = (bool)__this->___trackResurrection_1;
		NullCheck(L_2);
		SerializationInfo_AddValue_mC52253CB19C98F82A26E32C941F8F20E106D4C0D(L_2, _stringLiteral7D20B8219CA0491872B2E811B262066A5DD875A7, L_3, NULL);
		GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC* L_4 = (GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC*)(&__this->___handle_0);
		bool L_5;
		L_5 = GCHandle_get_IsAllocated_m241908103D8D867E11CCAB73C918729825E86843(L_4, NULL);
		if (!L_5)
		{
			goto IL_0043;
		}
	}
	{
		SerializationInfo_t3C47F63E24BEB9FCE2DC6309E027F238DC5C5E37* L_6 = ___info0;
		GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC* L_7 = (GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC*)(&__this->___handle_0);
		RuntimeObject* L_8;
		L_8 = GCHandle_get_Target_m481F9508DA5E384D33CD1F4450060DC56BBD4CD5(L_7, NULL);
		NullCheck(L_6);
		SerializationInfo_AddValue_m28FE9B110F21DDB8FF5F5E35A0EABD659DB22C2F(L_6, _stringLiteral5CA6E7C0AE72196B2817D93A78C719652EC691C0, L_8, NULL);
		return;
	}

IL_0043:
	{
		SerializationInfo_t3C47F63E24BEB9FCE2DC6309E027F238DC5C5E37* L_9 = ___info0;
		NullCheck(L_9);
		SerializationInfo_AddValue_m28FE9B110F21DDB8FF5F5E35A0EABD659DB22C2F(L_9, _stringLiteral5CA6E7C0AE72196B2817D93A78C719652EC691C0, NULL, NULL);
		return;
	}
}
// System.Void System.WeakReference`1<System.Object>::Finalize()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WeakReference_1_Finalize_m22CABA82C1F2B17A77E275483306A0DADECAF151_gshared (WeakReference_1_tED795563AD26F795CED3BBCD488AB1694E385BCE* __this, const RuntimeMethod* method) 
{
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_000d:
			{// begin finally (depth: 1)
				NullCheck((RuntimeObject*)__this);
				Object_Finalize_mC98C96301CCABFE00F1A7EF8E15DF507CACD42B2((RuntimeObject*)__this, NULL);
				return;
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC* L_0 = (GCHandle_tC44F6F72EE68BD4CFABA24309DA7A179D41127DC*)(&__this->___handle_0);
			GCHandle_Free_m1320A260E487EB1EA6D95F9E54BFFCB5A4EF83A3(L_0, NULL);
			goto IL_0014;
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0014:
	{
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void System.Threading.Tasks.Task/WhenAllPromise`1<System.Boolean>::.ctor(System.Threading.Tasks.Task`1<T>[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhenAllPromise_1__ctor_m5106A8A0FC74E9D4B9CEB97865906FBC928CBBC0_gshared (WhenAllPromise_1_t97739DA26ABE51AE956DAFF6EB494F6A4F190235* __this, Task_1U5BU5D_tBA390D600BBF7458FC9BD634B8F6E52B5D753C91* ___tasks0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralD68A68585C704F4E081A45928451F951EF468E68);
		s_Il2CppMethodInitialized = true;
	}
	Task_1U5BU5D_tBA390D600BBF7458FC9BD634B8F6E52B5D753C91* V_0 = NULL;
	int32_t V_1 = 0;
	Task_1_t824317F4B958F7512E8F7300511752937A6C6043* V_2 = NULL;
	{
		Task_1__ctor_m7894EC8EE695D23CC55A13A5303EBB5CC53FB12F((Task_1_t69C91E4C5122CE3A171C7090781C6B14BE5D63DA*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 0));
		Task_1U5BU5D_tBA390D600BBF7458FC9BD634B8F6E52B5D753C91* L_0 = ___tasks0;
		__this->___m_tasks_23 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_tasks_23), (void*)L_0);
		Task_1U5BU5D_tBA390D600BBF7458FC9BD634B8F6E52B5D753C91* L_1 = ___tasks0;
		NullCheck(L_1);
		__this->___m_count_24 = ((int32_t)(((RuntimeArray*)L_1)->max_length));
		il2cpp_codegen_runtime_class_init_inline(DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var);
		bool L_2;
		L_2 = DebuggerSupport_get_LoggingOn_mD838646A5A048C62BAB034257EF0F2F852AF0ABB(NULL);
		if (!L_2)
		{
			goto IL_002b;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var);
		DebuggerSupport_TraceOperationCreation_m311097028455DBEED9480F6315649693464F2C06((int32_t)0, (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this, _stringLiteralD68A68585C704F4E081A45928451F951EF468E68, (uint64_t)((int64_t)0), NULL);
	}

IL_002b:
	{
		il2cpp_codegen_runtime_class_init_inline(DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var);
		DebuggerSupport_AddToActiveTasks_mF592C309CB2AE9C85563BE114DDDB6D226AD9E3E_inline((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this, NULL);
		Task_1U5BU5D_tBA390D600BBF7458FC9BD634B8F6E52B5D753C91* L_3 = ___tasks0;
		V_0 = L_3;
		V_1 = 0;
		goto IL_0057;
	}

IL_0037:
	{
		Task_1U5BU5D_tBA390D600BBF7458FC9BD634B8F6E52B5D753C91* L_4 = V_0;
		int32_t L_5 = V_1;
		NullCheck(L_4);
		int32_t L_6 = L_5;
		Task_1_t824317F4B958F7512E8F7300511752937A6C6043* L_7 = (L_4)->GetAt(static_cast<il2cpp_array_size_t>(L_6));
		V_2 = L_7;
		Task_1_t824317F4B958F7512E8F7300511752937A6C6043* L_8 = V_2;
		NullCheck((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_8);
		bool L_9;
		L_9 = Task_get_IsCompleted_m942D6D536545EF059089398B19435591561BB831((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_8, NULL);
		if (!L_9)
		{
			goto IL_004c;
		}
	}
	{
		Task_1_t824317F4B958F7512E8F7300511752937A6C6043* L_10 = V_2;
		WhenAllPromise_1_Invoke_m1AE233CB3A4177EE300CDAC3308A625AC299A5D4(__this, (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_10, il2cpp_rgctx_method(method->klass->rgctx_data, 4));
		goto IL_0053;
	}

IL_004c:
	{
		Task_1_t824317F4B958F7512E8F7300511752937A6C6043* L_11 = V_2;
		NullCheck((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_11);
		Task_AddCompletionAction_m77811E563FC391FF0F51DD14AC67D35318378CDA((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_11, (RuntimeObject*)__this, NULL);
	}

IL_0053:
	{
		int32_t L_12 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_12, 1));
	}

IL_0057:
	{
		int32_t L_13 = V_1;
		Task_1U5BU5D_tBA390D600BBF7458FC9BD634B8F6E52B5D753C91* L_14 = V_0;
		NullCheck(L_14);
		if ((((int32_t)L_13) < ((int32_t)((int32_t)(((RuntimeArray*)L_14)->max_length)))))
		{
			goto IL_0037;
		}
	}
	{
		return;
	}
}
// System.Void System.Threading.Tasks.Task/WhenAllPromise`1<System.Boolean>::Invoke(System.Threading.Tasks.Task)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhenAllPromise_1_Invoke_m1AE233CB3A4177EE300CDAC3308A625AC299A5D4_gshared (WhenAllPromise_1_t97739DA26ABE51AE956DAFF6EB494F6A4F190235* __this, Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* ___ignored0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&LowLevelListWithIList_1__ctor_m0E3A007F2F2CA220DB5C20F85BD60FE2F4A045E2_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&LowLevelList_1_AddRange_mD5D6366F78D1A613E03AE6A45654DE4C2A6C51BE_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	BooleanU5BU5D_tD317D27C31DB892BE79FAE3AEBC0B3FFB73DE9B4* V_0 = NULL;
	LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4* V_1 = NULL;
	Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* V_2 = NULL;
	int32_t V_3 = 0;
	Task_1_t824317F4B958F7512E8F7300511752937A6C6043* V_4 = NULL;
	{
		il2cpp_codegen_runtime_class_init_inline(DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var);
		bool L_0;
		L_0 = DebuggerSupport_get_LoggingOn_mD838646A5A048C62BAB034257EF0F2F852AF0ABB(NULL);
		if (!L_0)
		{
			goto IL_000f;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var);
		DebuggerSupport_TraceOperationRelation_m940A2FF274D08177EA7D83FAAE4492DAC0CFE16D((int32_t)1, (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this, (int32_t)1, NULL);
	}

IL_000f:
	{
		int32_t* L_1 = (int32_t*)(&__this->___m_count_24);
		int32_t L_2;
		L_2 = Interlocked_Decrement_m6AFAD2E874CBDA373B1EF7572F11D6E91813E75D(L_1, NULL);
		if (L_2)
		{
			goto IL_00ea;
		}
	}
	{
		Task_1U5BU5D_tBA390D600BBF7458FC9BD634B8F6E52B5D753C91* L_3 = (Task_1U5BU5D_tBA390D600BBF7458FC9BD634B8F6E52B5D753C91*)__this->___m_tasks_23;
		NullCheck(L_3);
		BooleanU5BU5D_tD317D27C31DB892BE79FAE3AEBC0B3FFB73DE9B4* L_4 = (BooleanU5BU5D_tD317D27C31DB892BE79FAE3AEBC0B3FFB73DE9B4*)(BooleanU5BU5D_tD317D27C31DB892BE79FAE3AEBC0B3FFB73DE9B4*)SZArrayNew(il2cpp_rgctx_data(method->klass->rgctx_data, 6), (uint32_t)((int32_t)(((RuntimeArray*)L_3)->max_length)));
		V_0 = L_4;
		V_1 = (LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4*)NULL;
		V_2 = (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)NULL;
		V_3 = 0;
		goto IL_009f;
	}

IL_0035:
	{
		Task_1U5BU5D_tBA390D600BBF7458FC9BD634B8F6E52B5D753C91* L_5 = (Task_1U5BU5D_tBA390D600BBF7458FC9BD634B8F6E52B5D753C91*)__this->___m_tasks_23;
		int32_t L_6 = V_3;
		NullCheck(L_5);
		int32_t L_7 = L_6;
		Task_1_t824317F4B958F7512E8F7300511752937A6C6043* L_8 = (L_5)->GetAt(static_cast<il2cpp_array_size_t>(L_7));
		V_4 = L_8;
		Task_1_t824317F4B958F7512E8F7300511752937A6C6043* L_9 = V_4;
		NullCheck((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_9);
		bool L_10;
		L_10 = Task_get_IsFaulted_mC0AD3EA4EAF3B47C1F5FE9624541F0A00B9426D9((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_9, NULL);
		if (!L_10)
		{
			goto IL_0060;
		}
	}
	{
		LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4* L_11 = V_1;
		if (L_11)
		{
			goto IL_0051;
		}
	}
	{
		LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4* L_12 = (LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4*)il2cpp_codegen_object_new(LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4_il2cpp_TypeInfo_var);
		NullCheck(L_12);
		LowLevelListWithIList_1__ctor_m0E3A007F2F2CA220DB5C20F85BD60FE2F4A045E2(L_12, LowLevelListWithIList_1__ctor_m0E3A007F2F2CA220DB5C20F85BD60FE2F4A045E2_RuntimeMethod_var);
		V_1 = L_12;
	}

IL_0051:
	{
		LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4* L_13 = V_1;
		Task_1_t824317F4B958F7512E8F7300511752937A6C6043* L_14 = V_4;
		NullCheck((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_14);
		ReadOnlyCollection_1_t7E3BC8E94E9BC82C2FD0D77A76BF08AC79C2CECD* L_15;
		L_15 = Task_GetExceptionDispatchInfos_m2E8811FF2E0CDBC4BFE281A4822C6D8452832831((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_14, NULL);
		NullCheck((LowLevelList_1_tD1EA453996325637EAF8C2A867D4DED46603298F*)L_13);
		LowLevelList_1_AddRange_mD5D6366F78D1A613E03AE6A45654DE4C2A6C51BE((LowLevelList_1_tD1EA453996325637EAF8C2A867D4DED46603298F*)L_13, (RuntimeObject*)L_15, LowLevelList_1_AddRange_mD5D6366F78D1A613E03AE6A45654DE4C2A6C51BE_RuntimeMethod_var);
		goto IL_0080;
	}

IL_0060:
	{
		Task_1_t824317F4B958F7512E8F7300511752937A6C6043* L_16 = V_4;
		NullCheck((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_16);
		bool L_17;
		L_17 = Task_get_IsCanceled_m96A8D3F85158A9CB3AEA50A00A55BE4E0F0E21FA((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_16, NULL);
		if (!L_17)
		{
			goto IL_0071;
		}
	}
	{
		Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* L_18 = V_2;
		if (L_18)
		{
			goto IL_0080;
		}
	}
	{
		Task_1_t824317F4B958F7512E8F7300511752937A6C6043* L_19 = V_4;
		V_2 = (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_19;
		goto IL_0080;
	}

IL_0071:
	{
		BooleanU5BU5D_tD317D27C31DB892BE79FAE3AEBC0B3FFB73DE9B4* L_20 = V_0;
		int32_t L_21 = V_3;
		Task_1_t824317F4B958F7512E8F7300511752937A6C6043* L_22 = V_4;
		NullCheck(L_22);
		bool L_23;
		L_23 = Task_1_GetResultCore_mCE0DE2898E40C4E670E13C134F029C59AC4EA2FB(L_22, (bool)0, il2cpp_rgctx_method(method->klass->rgctx_data, 7));
		NullCheck(L_20);
		(L_20)->SetAt(static_cast<il2cpp_array_size_t>(L_21), (bool)L_23);
	}

IL_0080:
	{
		Task_1_t824317F4B958F7512E8F7300511752937A6C6043* L_24 = V_4;
		NullCheck((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_24);
		bool L_25;
		L_25 = Task_get_IsWaitNotificationEnabled_mF6950E2B28561EE2E57DECADAD63B485CA5DD3A8((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_24, NULL);
		if (!L_25)
		{
			goto IL_0092;
		}
	}
	{
		NullCheck((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this);
		Task_SetNotificationForWaitCompletion_m6B087B3B1E1B6911006874042808D4D7D9678AED((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this, (bool)1, NULL);
		goto IL_009b;
	}

IL_0092:
	{
		Task_1U5BU5D_tBA390D600BBF7458FC9BD634B8F6E52B5D753C91* L_26 = (Task_1U5BU5D_tBA390D600BBF7458FC9BD634B8F6E52B5D753C91*)__this->___m_tasks_23;
		int32_t L_27 = V_3;
		NullCheck(L_26);
		ArrayElementTypeCheck (L_26, NULL);
		(L_26)->SetAt(static_cast<il2cpp_array_size_t>(L_27), (Task_1_t824317F4B958F7512E8F7300511752937A6C6043*)NULL);
	}

IL_009b:
	{
		int32_t L_28 = V_3;
		V_3 = ((int32_t)il2cpp_codegen_add(L_28, 1));
	}

IL_009f:
	{
		int32_t L_29 = V_3;
		Task_1U5BU5D_tBA390D600BBF7458FC9BD634B8F6E52B5D753C91* L_30 = (Task_1U5BU5D_tBA390D600BBF7458FC9BD634B8F6E52B5D753C91*)__this->___m_tasks_23;
		NullCheck(L_30);
		if ((((int32_t)L_29) < ((int32_t)((int32_t)(((RuntimeArray*)L_30)->max_length)))))
		{
			goto IL_0035;
		}
	}
	{
		LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4* L_31 = V_1;
		if (!L_31)
		{
			goto IL_00b6;
		}
	}
	{
		LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4* L_32 = V_1;
		NullCheck((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this);
		bool L_33;
		L_33 = Task_TrySetException_m8336BA31D11EA84916A89EB8A7A0044D2D0EE94D((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this, (RuntimeObject*)L_32, NULL);
		return;
	}

IL_00b6:
	{
		Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* L_34 = V_2;
		if (!L_34)
		{
			goto IL_00cd;
		}
	}
	{
		Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* L_35 = V_2;
		NullCheck(L_35);
		CancellationToken_t51142D9C6D7C02D314DA34A6A7988C528992FFED L_36;
		L_36 = Task_get_CancellationToken_m459E6E4311018E389AC44E089CCB4ACDC252766A(L_35, NULL);
		Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* L_37 = V_2;
		NullCheck(L_37);
		ExceptionDispatchInfo_tD7AF19E75FEC22F4A8329FD1E9EDF96615CB2757* L_38;
		L_38 = Task_GetCancellationExceptionDispatchInfo_m190A98B306C8BCCB67F3D8B2E7B8BF75EAE63E34(L_37, NULL);
		NullCheck((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this);
		bool L_39;
		L_39 = Task_TrySetCanceled_m8E24757A8DD3AE5A856B64D87B447E08395A0771((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this, L_36, (RuntimeObject*)L_38, NULL);
		return;
	}

IL_00cd:
	{
		il2cpp_codegen_runtime_class_init_inline(DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var);
		bool L_40;
		L_40 = DebuggerSupport_get_LoggingOn_mD838646A5A048C62BAB034257EF0F2F852AF0ABB(NULL);
		if (!L_40)
		{
			goto IL_00dc;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var);
		DebuggerSupport_TraceOperationCompletion_m7047A96BCB7DC4835B38D1B965B4BC3049AF62CB((int32_t)0, (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this, (int32_t)1, NULL);
	}

IL_00dc:
	{
		il2cpp_codegen_runtime_class_init_inline(DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var);
		DebuggerSupport_RemoveFromActiveTasks_m19229D30DAA2447DDAB524F2A0E9718D070A1251_inline((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this, NULL);
		BooleanU5BU5D_tD317D27C31DB892BE79FAE3AEBC0B3FFB73DE9B4* L_41 = V_0;
		NullCheck((Task_1_t69C91E4C5122CE3A171C7090781C6B14BE5D63DA*)__this);
		bool L_42;
		L_42 = Task_1_TrySetResult_m69CB8B64C8114D46D75D13BA20D79AEE9C6FB159((Task_1_t69C91E4C5122CE3A171C7090781C6B14BE5D63DA*)__this, L_41, il2cpp_rgctx_method(method->klass->rgctx_data, 9));
	}

IL_00ea:
	{
		return;
	}
}
// System.Boolean System.Threading.Tasks.Task/WhenAllPromise`1<System.Boolean>::get_ShouldNotifyDebuggerOfWaitCompletion()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool WhenAllPromise_1_get_ShouldNotifyDebuggerOfWaitCompletion_m059FEC3E639EF2CBAF1E96F12278182B504A1925_gshared (WhenAllPromise_1_t97739DA26ABE51AE956DAFF6EB494F6A4F190235* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	TaskU5BU5D_t368E447BD9A179BA9A26BAAABF1BAE9CA79F60B3* V_0 = NULL;
	{
		NullCheck((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this);
		bool L_0;
		L_0 = Task_get_ShouldNotifyDebuggerOfWaitCompletion_m7AB76993855A98D7FA90003FBACF579C7D7E4534((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this, NULL);
		if (!L_0)
		{
			goto IL_0016;
		}
	}
	{
		Task_1U5BU5D_tBA390D600BBF7458FC9BD634B8F6E52B5D753C91* L_1 = (Task_1U5BU5D_tBA390D600BBF7458FC9BD634B8F6E52B5D753C91*)__this->___m_tasks_23;
		V_0 = (TaskU5BU5D_t368E447BD9A179BA9A26BAAABF1BAE9CA79F60B3*)L_1;
		TaskU5BU5D_t368E447BD9A179BA9A26BAAABF1BAE9CA79F60B3* L_2 = V_0;
		il2cpp_codegen_runtime_class_init_inline(Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572_il2cpp_TypeInfo_var);
		bool L_3;
		L_3 = Task_AnyTaskRequiresNotifyDebuggerOfWaitCompletion_m90C692B044AB99E76F7BF6A2BCD909DCDD01FBA3(L_2, NULL);
		return L_3;
	}

IL_0016:
	{
		return (bool)0;
	}
}
// System.Boolean System.Threading.Tasks.Task/WhenAllPromise`1<System.Boolean>::get_InvokeMayRunArbitraryCode()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool WhenAllPromise_1_get_InvokeMayRunArbitraryCode_mBB0F85469F4614E0A29C105554F4585DE2608E69_gshared (WhenAllPromise_1_t97739DA26ABE51AE956DAFF6EB494F6A4F190235* __this, const RuntimeMethod* method) 
{
	{
		return (bool)1;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void System.Threading.Tasks.Task/WhenAllPromise`1<System.Object>::.ctor(System.Threading.Tasks.Task`1<T>[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhenAllPromise_1__ctor_mFEF936314662E210EBE42AA92CCE6F9051AC433B_gshared (WhenAllPromise_1_t6B7D3ABF753EA8F6F7581C787A300C50CBA54FF0* __this, Task_1U5BU5D_tE516FE52659CC40D166035B14BAEFD6B275FBA1E* ___tasks0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralD68A68585C704F4E081A45928451F951EF468E68);
		s_Il2CppMethodInitialized = true;
	}
	Task_1U5BU5D_tE516FE52659CC40D166035B14BAEFD6B275FBA1E* V_0 = NULL;
	int32_t V_1 = 0;
	Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2* V_2 = NULL;
	{
		Task_1__ctor_m03A743E024D677111A87084460EE475C07C47C83((Task_1_t5B1ACE26BA285FAD19DF6E5F61190F03850816CB*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 0));
		Task_1U5BU5D_tE516FE52659CC40D166035B14BAEFD6B275FBA1E* L_0 = ___tasks0;
		__this->___m_tasks_23 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_tasks_23), (void*)L_0);
		Task_1U5BU5D_tE516FE52659CC40D166035B14BAEFD6B275FBA1E* L_1 = ___tasks0;
		NullCheck(L_1);
		__this->___m_count_24 = ((int32_t)(((RuntimeArray*)L_1)->max_length));
		il2cpp_codegen_runtime_class_init_inline(DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var);
		bool L_2;
		L_2 = DebuggerSupport_get_LoggingOn_mD838646A5A048C62BAB034257EF0F2F852AF0ABB(NULL);
		if (!L_2)
		{
			goto IL_002b;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var);
		DebuggerSupport_TraceOperationCreation_m311097028455DBEED9480F6315649693464F2C06((int32_t)0, (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this, _stringLiteralD68A68585C704F4E081A45928451F951EF468E68, (uint64_t)((int64_t)0), NULL);
	}

IL_002b:
	{
		il2cpp_codegen_runtime_class_init_inline(DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var);
		DebuggerSupport_AddToActiveTasks_mF592C309CB2AE9C85563BE114DDDB6D226AD9E3E_inline((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this, NULL);
		Task_1U5BU5D_tE516FE52659CC40D166035B14BAEFD6B275FBA1E* L_3 = ___tasks0;
		V_0 = L_3;
		V_1 = 0;
		goto IL_0057;
	}

IL_0037:
	{
		Task_1U5BU5D_tE516FE52659CC40D166035B14BAEFD6B275FBA1E* L_4 = V_0;
		int32_t L_5 = V_1;
		NullCheck(L_4);
		int32_t L_6 = L_5;
		Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2* L_7 = (L_4)->GetAt(static_cast<il2cpp_array_size_t>(L_6));
		V_2 = L_7;
		Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2* L_8 = V_2;
		NullCheck((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_8);
		bool L_9;
		L_9 = Task_get_IsCompleted_m942D6D536545EF059089398B19435591561BB831((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_8, NULL);
		if (!L_9)
		{
			goto IL_004c;
		}
	}
	{
		Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2* L_10 = V_2;
		WhenAllPromise_1_Invoke_mC22438FD090403404288BA997992DE80EE2FA153(__this, (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_10, il2cpp_rgctx_method(method->klass->rgctx_data, 4));
		goto IL_0053;
	}

IL_004c:
	{
		Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2* L_11 = V_2;
		NullCheck((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_11);
		Task_AddCompletionAction_m77811E563FC391FF0F51DD14AC67D35318378CDA((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_11, (RuntimeObject*)__this, NULL);
	}

IL_0053:
	{
		int32_t L_12 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_12, 1));
	}

IL_0057:
	{
		int32_t L_13 = V_1;
		Task_1U5BU5D_tE516FE52659CC40D166035B14BAEFD6B275FBA1E* L_14 = V_0;
		NullCheck(L_14);
		if ((((int32_t)L_13) < ((int32_t)((int32_t)(((RuntimeArray*)L_14)->max_length)))))
		{
			goto IL_0037;
		}
	}
	{
		return;
	}
}
// System.Void System.Threading.Tasks.Task/WhenAllPromise`1<System.Object>::Invoke(System.Threading.Tasks.Task)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhenAllPromise_1_Invoke_mC22438FD090403404288BA997992DE80EE2FA153_gshared (WhenAllPromise_1_t6B7D3ABF753EA8F6F7581C787A300C50CBA54FF0* __this, Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* ___ignored0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&LowLevelListWithIList_1__ctor_m0E3A007F2F2CA220DB5C20F85BD60FE2F4A045E2_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&LowLevelList_1_AddRange_mD5D6366F78D1A613E03AE6A45654DE4C2A6C51BE_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* V_0 = NULL;
	LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4* V_1 = NULL;
	Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* V_2 = NULL;
	int32_t V_3 = 0;
	Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2* V_4 = NULL;
	{
		il2cpp_codegen_runtime_class_init_inline(DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var);
		bool L_0;
		L_0 = DebuggerSupport_get_LoggingOn_mD838646A5A048C62BAB034257EF0F2F852AF0ABB(NULL);
		if (!L_0)
		{
			goto IL_000f;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var);
		DebuggerSupport_TraceOperationRelation_m940A2FF274D08177EA7D83FAAE4492DAC0CFE16D((int32_t)1, (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this, (int32_t)1, NULL);
	}

IL_000f:
	{
		int32_t* L_1 = (int32_t*)(&__this->___m_count_24);
		int32_t L_2;
		L_2 = Interlocked_Decrement_m6AFAD2E874CBDA373B1EF7572F11D6E91813E75D(L_1, NULL);
		if (L_2)
		{
			goto IL_00ea;
		}
	}
	{
		Task_1U5BU5D_tE516FE52659CC40D166035B14BAEFD6B275FBA1E* L_3 = (Task_1U5BU5D_tE516FE52659CC40D166035B14BAEFD6B275FBA1E*)__this->___m_tasks_23;
		NullCheck(L_3);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_4 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(il2cpp_rgctx_data(method->klass->rgctx_data, 6), (uint32_t)((int32_t)(((RuntimeArray*)L_3)->max_length)));
		V_0 = L_4;
		V_1 = (LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4*)NULL;
		V_2 = (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)NULL;
		V_3 = 0;
		goto IL_009f;
	}

IL_0035:
	{
		Task_1U5BU5D_tE516FE52659CC40D166035B14BAEFD6B275FBA1E* L_5 = (Task_1U5BU5D_tE516FE52659CC40D166035B14BAEFD6B275FBA1E*)__this->___m_tasks_23;
		int32_t L_6 = V_3;
		NullCheck(L_5);
		int32_t L_7 = L_6;
		Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2* L_8 = (L_5)->GetAt(static_cast<il2cpp_array_size_t>(L_7));
		V_4 = L_8;
		Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2* L_9 = V_4;
		NullCheck((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_9);
		bool L_10;
		L_10 = Task_get_IsFaulted_mC0AD3EA4EAF3B47C1F5FE9624541F0A00B9426D9((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_9, NULL);
		if (!L_10)
		{
			goto IL_0060;
		}
	}
	{
		LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4* L_11 = V_1;
		if (L_11)
		{
			goto IL_0051;
		}
	}
	{
		LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4* L_12 = (LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4*)il2cpp_codegen_object_new(LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4_il2cpp_TypeInfo_var);
		NullCheck(L_12);
		LowLevelListWithIList_1__ctor_m0E3A007F2F2CA220DB5C20F85BD60FE2F4A045E2(L_12, LowLevelListWithIList_1__ctor_m0E3A007F2F2CA220DB5C20F85BD60FE2F4A045E2_RuntimeMethod_var);
		V_1 = L_12;
	}

IL_0051:
	{
		LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4* L_13 = V_1;
		Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2* L_14 = V_4;
		NullCheck((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_14);
		ReadOnlyCollection_1_t7E3BC8E94E9BC82C2FD0D77A76BF08AC79C2CECD* L_15;
		L_15 = Task_GetExceptionDispatchInfos_m2E8811FF2E0CDBC4BFE281A4822C6D8452832831((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_14, NULL);
		NullCheck((LowLevelList_1_tD1EA453996325637EAF8C2A867D4DED46603298F*)L_13);
		LowLevelList_1_AddRange_mD5D6366F78D1A613E03AE6A45654DE4C2A6C51BE((LowLevelList_1_tD1EA453996325637EAF8C2A867D4DED46603298F*)L_13, (RuntimeObject*)L_15, LowLevelList_1_AddRange_mD5D6366F78D1A613E03AE6A45654DE4C2A6C51BE_RuntimeMethod_var);
		goto IL_0080;
	}

IL_0060:
	{
		Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2* L_16 = V_4;
		NullCheck((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_16);
		bool L_17;
		L_17 = Task_get_IsCanceled_m96A8D3F85158A9CB3AEA50A00A55BE4E0F0E21FA((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_16, NULL);
		if (!L_17)
		{
			goto IL_0071;
		}
	}
	{
		Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* L_18 = V_2;
		if (L_18)
		{
			goto IL_0080;
		}
	}
	{
		Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2* L_19 = V_4;
		V_2 = (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_19;
		goto IL_0080;
	}

IL_0071:
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_20 = V_0;
		int32_t L_21 = V_3;
		Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2* L_22 = V_4;
		NullCheck(L_22);
		RuntimeObject* L_23;
		L_23 = Task_1_GetResultCore_mA6141C79F2DD78034AE5BC0311D4D143A5C320B9(L_22, (bool)0, il2cpp_rgctx_method(method->klass->rgctx_data, 7));
		NullCheck(L_20);
		(L_20)->SetAt(static_cast<il2cpp_array_size_t>(L_21), (RuntimeObject*)L_23);
	}

IL_0080:
	{
		Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2* L_24 = V_4;
		NullCheck((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_24);
		bool L_25;
		L_25 = Task_get_IsWaitNotificationEnabled_mF6950E2B28561EE2E57DECADAD63B485CA5DD3A8((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_24, NULL);
		if (!L_25)
		{
			goto IL_0092;
		}
	}
	{
		NullCheck((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this);
		Task_SetNotificationForWaitCompletion_m6B087B3B1E1B6911006874042808D4D7D9678AED((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this, (bool)1, NULL);
		goto IL_009b;
	}

IL_0092:
	{
		Task_1U5BU5D_tE516FE52659CC40D166035B14BAEFD6B275FBA1E* L_26 = (Task_1U5BU5D_tE516FE52659CC40D166035B14BAEFD6B275FBA1E*)__this->___m_tasks_23;
		int32_t L_27 = V_3;
		NullCheck(L_26);
		ArrayElementTypeCheck (L_26, NULL);
		(L_26)->SetAt(static_cast<il2cpp_array_size_t>(L_27), (Task_1_t0C4CD3A5BB93A184420D73218644C56C70FDA7E2*)NULL);
	}

IL_009b:
	{
		int32_t L_28 = V_3;
		V_3 = ((int32_t)il2cpp_codegen_add(L_28, 1));
	}

IL_009f:
	{
		int32_t L_29 = V_3;
		Task_1U5BU5D_tE516FE52659CC40D166035B14BAEFD6B275FBA1E* L_30 = (Task_1U5BU5D_tE516FE52659CC40D166035B14BAEFD6B275FBA1E*)__this->___m_tasks_23;
		NullCheck(L_30);
		if ((((int32_t)L_29) < ((int32_t)((int32_t)(((RuntimeArray*)L_30)->max_length)))))
		{
			goto IL_0035;
		}
	}
	{
		LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4* L_31 = V_1;
		if (!L_31)
		{
			goto IL_00b6;
		}
	}
	{
		LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4* L_32 = V_1;
		NullCheck((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this);
		bool L_33;
		L_33 = Task_TrySetException_m8336BA31D11EA84916A89EB8A7A0044D2D0EE94D((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this, (RuntimeObject*)L_32, NULL);
		return;
	}

IL_00b6:
	{
		Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* L_34 = V_2;
		if (!L_34)
		{
			goto IL_00cd;
		}
	}
	{
		Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* L_35 = V_2;
		NullCheck(L_35);
		CancellationToken_t51142D9C6D7C02D314DA34A6A7988C528992FFED L_36;
		L_36 = Task_get_CancellationToken_m459E6E4311018E389AC44E089CCB4ACDC252766A(L_35, NULL);
		Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* L_37 = V_2;
		NullCheck(L_37);
		ExceptionDispatchInfo_tD7AF19E75FEC22F4A8329FD1E9EDF96615CB2757* L_38;
		L_38 = Task_GetCancellationExceptionDispatchInfo_m190A98B306C8BCCB67F3D8B2E7B8BF75EAE63E34(L_37, NULL);
		NullCheck((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this);
		bool L_39;
		L_39 = Task_TrySetCanceled_m8E24757A8DD3AE5A856B64D87B447E08395A0771((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this, L_36, (RuntimeObject*)L_38, NULL);
		return;
	}

IL_00cd:
	{
		il2cpp_codegen_runtime_class_init_inline(DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var);
		bool L_40;
		L_40 = DebuggerSupport_get_LoggingOn_mD838646A5A048C62BAB034257EF0F2F852AF0ABB(NULL);
		if (!L_40)
		{
			goto IL_00dc;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var);
		DebuggerSupport_TraceOperationCompletion_m7047A96BCB7DC4835B38D1B965B4BC3049AF62CB((int32_t)0, (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this, (int32_t)1, NULL);
	}

IL_00dc:
	{
		il2cpp_codegen_runtime_class_init_inline(DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var);
		DebuggerSupport_RemoveFromActiveTasks_m19229D30DAA2447DDAB524F2A0E9718D070A1251_inline((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this, NULL);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_41 = V_0;
		NullCheck((Task_1_t5B1ACE26BA285FAD19DF6E5F61190F03850816CB*)__this);
		bool L_42;
		L_42 = Task_1_TrySetResult_mAF6B2C87E8E79DC5099DFAD16F0FC5AB4AA2D1E4((Task_1_t5B1ACE26BA285FAD19DF6E5F61190F03850816CB*)__this, L_41, il2cpp_rgctx_method(method->klass->rgctx_data, 9));
	}

IL_00ea:
	{
		return;
	}
}
// System.Boolean System.Threading.Tasks.Task/WhenAllPromise`1<System.Object>::get_ShouldNotifyDebuggerOfWaitCompletion()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool WhenAllPromise_1_get_ShouldNotifyDebuggerOfWaitCompletion_m93CFD1E55D9F6225C7D05F2B8EA17588268CBC0E_gshared (WhenAllPromise_1_t6B7D3ABF753EA8F6F7581C787A300C50CBA54FF0* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	TaskU5BU5D_t368E447BD9A179BA9A26BAAABF1BAE9CA79F60B3* V_0 = NULL;
	{
		NullCheck((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this);
		bool L_0;
		L_0 = Task_get_ShouldNotifyDebuggerOfWaitCompletion_m7AB76993855A98D7FA90003FBACF579C7D7E4534((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this, NULL);
		if (!L_0)
		{
			goto IL_0016;
		}
	}
	{
		Task_1U5BU5D_tE516FE52659CC40D166035B14BAEFD6B275FBA1E* L_1 = (Task_1U5BU5D_tE516FE52659CC40D166035B14BAEFD6B275FBA1E*)__this->___m_tasks_23;
		V_0 = (TaskU5BU5D_t368E447BD9A179BA9A26BAAABF1BAE9CA79F60B3*)L_1;
		TaskU5BU5D_t368E447BD9A179BA9A26BAAABF1BAE9CA79F60B3* L_2 = V_0;
		il2cpp_codegen_runtime_class_init_inline(Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572_il2cpp_TypeInfo_var);
		bool L_3;
		L_3 = Task_AnyTaskRequiresNotifyDebuggerOfWaitCompletion_m90C692B044AB99E76F7BF6A2BCD909DCDD01FBA3(L_2, NULL);
		return L_3;
	}

IL_0016:
	{
		return (bool)0;
	}
}
// System.Boolean System.Threading.Tasks.Task/WhenAllPromise`1<System.Object>::get_InvokeMayRunArbitraryCode()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool WhenAllPromise_1_get_InvokeMayRunArbitraryCode_mD1154032DD51F1EEAAF40B2B45823C5CC7C526CA_gshared (WhenAllPromise_1_t6B7D3ABF753EA8F6F7581C787A300C50CBA54FF0* __this, const RuntimeMethod* method) 
{
	{
		return (bool)1;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void System.Threading.Tasks.Task/WhenAllPromise`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::.ctor(System.Threading.Tasks.Task`1<T>[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhenAllPromise_1__ctor_m2EAEF619758A6318F287CFE412AC880E1511BE04_gshared (WhenAllPromise_1_tF47371F9AE700EE2F63999402265300771AAFDC3* __this, Task_1U5BU5D_t46133A6F873A9EC9FC75DAB1315ABC8025565F09* ___tasks0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralD68A68585C704F4E081A45928451F951EF468E68);
		s_Il2CppMethodInitialized = true;
	}
	Task_1U5BU5D_t46133A6F873A9EC9FC75DAB1315ABC8025565F09* V_0 = NULL;
	int32_t V_1 = 0;
	Task_1_tDF1FF540D7D2248A08580387A39717B7FB7A9CF9* V_2 = NULL;
	{
		InvokerActionInvoker0::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 0)), il2cpp_rgctx_method(method->klass->rgctx_data, 0), (Task_1_tEA323ABE4B135A0CC68B2D7C3498CC3DA6B8D802*)__this);
		Task_1U5BU5D_t46133A6F873A9EC9FC75DAB1315ABC8025565F09* L_0 = ___tasks0;
		__this->___m_tasks_23 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_tasks_23), (void*)L_0);
		Task_1U5BU5D_t46133A6F873A9EC9FC75DAB1315ABC8025565F09* L_1 = ___tasks0;
		NullCheck(L_1);
		__this->___m_count_24 = ((int32_t)(((RuntimeArray*)L_1)->max_length));
		il2cpp_codegen_runtime_class_init_inline(DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var);
		bool L_2;
		L_2 = DebuggerSupport_get_LoggingOn_mD838646A5A048C62BAB034257EF0F2F852AF0ABB(NULL);
		if (!L_2)
		{
			goto IL_002b;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var);
		DebuggerSupport_TraceOperationCreation_m311097028455DBEED9480F6315649693464F2C06((int32_t)0, (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this, _stringLiteralD68A68585C704F4E081A45928451F951EF468E68, (uint64_t)((int64_t)0), NULL);
	}

IL_002b:
	{
		il2cpp_codegen_runtime_class_init_inline(DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var);
		DebuggerSupport_AddToActiveTasks_mF592C309CB2AE9C85563BE114DDDB6D226AD9E3E_inline((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this, NULL);
		Task_1U5BU5D_t46133A6F873A9EC9FC75DAB1315ABC8025565F09* L_3 = ___tasks0;
		V_0 = L_3;
		V_1 = 0;
		goto IL_0057;
	}

IL_0037:
	{
		Task_1U5BU5D_t46133A6F873A9EC9FC75DAB1315ABC8025565F09* L_4 = V_0;
		int32_t L_5 = V_1;
		NullCheck(L_4);
		int32_t L_6 = L_5;
		Task_1_tDF1FF540D7D2248A08580387A39717B7FB7A9CF9* L_7 = (L_4)->GetAt(static_cast<il2cpp_array_size_t>(L_6));
		V_2 = L_7;
		Task_1_tDF1FF540D7D2248A08580387A39717B7FB7A9CF9* L_8 = V_2;
		NullCheck((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_8);
		bool L_9;
		L_9 = Task_get_IsCompleted_m942D6D536545EF059089398B19435591561BB831((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_8, NULL);
		if (!L_9)
		{
			goto IL_004c;
		}
	}
	{
		Task_1_tDF1FF540D7D2248A08580387A39717B7FB7A9CF9* L_10 = V_2;
		InvokerActionInvoker1< Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 4)), il2cpp_rgctx_method(method->klass->rgctx_data, 4), __this, (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_10);
		goto IL_0053;
	}

IL_004c:
	{
		Task_1_tDF1FF540D7D2248A08580387A39717B7FB7A9CF9* L_11 = V_2;
		NullCheck((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_11);
		Task_AddCompletionAction_m77811E563FC391FF0F51DD14AC67D35318378CDA((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_11, (RuntimeObject*)__this, NULL);
	}

IL_0053:
	{
		int32_t L_12 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_12, 1));
	}

IL_0057:
	{
		int32_t L_13 = V_1;
		Task_1U5BU5D_t46133A6F873A9EC9FC75DAB1315ABC8025565F09* L_14 = V_0;
		NullCheck(L_14);
		if ((((int32_t)L_13) < ((int32_t)((int32_t)(((RuntimeArray*)L_14)->max_length)))))
		{
			goto IL_0037;
		}
	}
	{
		return;
	}
}
// System.Void System.Threading.Tasks.Task/WhenAllPromise`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::Invoke(System.Threading.Tasks.Task)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhenAllPromise_1_Invoke_m832166DCB9FC1E3C21A24884F5FC98D229DCB9B8_gshared (WhenAllPromise_1_tF47371F9AE700EE2F63999402265300771AAFDC3* __this, Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* ___ignored0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&LowLevelListWithIList_1__ctor_m0E3A007F2F2CA220DB5C20F85BD60FE2F4A045E2_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&LowLevelList_1_AddRange_mD5D6366F78D1A613E03AE6A45654DE4C2A6C51BE_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	// sizeof(T)
	const uint32_t SizeOf_T_tE4830CEB11D3DDB28FE7E64F22C7869BD26E6FCF = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->klass->rgctx_data, 8));
	// T
	const Il2CppFullySharedGenericAny L_23 = alloca(SizeOf_T_tE4830CEB11D3DDB28FE7E64F22C7869BD26E6FCF);
	__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* V_0 = NULL;
	LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4* V_1 = NULL;
	Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* V_2 = NULL;
	int32_t V_3 = 0;
	Task_1_tDF1FF540D7D2248A08580387A39717B7FB7A9CF9* V_4 = NULL;
	{
		il2cpp_codegen_runtime_class_init_inline(DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var);
		bool L_0;
		L_0 = DebuggerSupport_get_LoggingOn_mD838646A5A048C62BAB034257EF0F2F852AF0ABB(NULL);
		if (!L_0)
		{
			goto IL_000f;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var);
		DebuggerSupport_TraceOperationRelation_m940A2FF274D08177EA7D83FAAE4492DAC0CFE16D((int32_t)1, (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this, (int32_t)1, NULL);
	}

IL_000f:
	{
		int32_t* L_1 = (int32_t*)(&__this->___m_count_24);
		int32_t L_2;
		L_2 = Interlocked_Decrement_m6AFAD2E874CBDA373B1EF7572F11D6E91813E75D(L_1, NULL);
		if (L_2)
		{
			goto IL_00ea;
		}
	}
	{
		Task_1U5BU5D_t46133A6F873A9EC9FC75DAB1315ABC8025565F09* L_3 = (Task_1U5BU5D_t46133A6F873A9EC9FC75DAB1315ABC8025565F09*)__this->___m_tasks_23;
		NullCheck(L_3);
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_4 = (__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC*)(__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC*)SZArrayNew(il2cpp_rgctx_data(method->klass->rgctx_data, 6), (uint32_t)((int32_t)(((RuntimeArray*)L_3)->max_length)));
		V_0 = L_4;
		V_1 = (LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4*)NULL;
		V_2 = (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)NULL;
		V_3 = 0;
		goto IL_009f;
	}

IL_0035:
	{
		Task_1U5BU5D_t46133A6F873A9EC9FC75DAB1315ABC8025565F09* L_5 = (Task_1U5BU5D_t46133A6F873A9EC9FC75DAB1315ABC8025565F09*)__this->___m_tasks_23;
		int32_t L_6 = V_3;
		NullCheck(L_5);
		int32_t L_7 = L_6;
		Task_1_tDF1FF540D7D2248A08580387A39717B7FB7A9CF9* L_8 = (L_5)->GetAt(static_cast<il2cpp_array_size_t>(L_7));
		V_4 = L_8;
		Task_1_tDF1FF540D7D2248A08580387A39717B7FB7A9CF9* L_9 = V_4;
		NullCheck((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_9);
		bool L_10;
		L_10 = Task_get_IsFaulted_mC0AD3EA4EAF3B47C1F5FE9624541F0A00B9426D9((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_9, NULL);
		if (!L_10)
		{
			goto IL_0060;
		}
	}
	{
		LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4* L_11 = V_1;
		if (L_11)
		{
			goto IL_0051;
		}
	}
	{
		LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4* L_12 = (LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4*)il2cpp_codegen_object_new(LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4_il2cpp_TypeInfo_var);
		NullCheck(L_12);
		LowLevelListWithIList_1__ctor_m0E3A007F2F2CA220DB5C20F85BD60FE2F4A045E2(L_12, LowLevelListWithIList_1__ctor_m0E3A007F2F2CA220DB5C20F85BD60FE2F4A045E2_RuntimeMethod_var);
		V_1 = L_12;
	}

IL_0051:
	{
		LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4* L_13 = V_1;
		Task_1_tDF1FF540D7D2248A08580387A39717B7FB7A9CF9* L_14 = V_4;
		NullCheck((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_14);
		ReadOnlyCollection_1_t7E3BC8E94E9BC82C2FD0D77A76BF08AC79C2CECD* L_15;
		L_15 = Task_GetExceptionDispatchInfos_m2E8811FF2E0CDBC4BFE281A4822C6D8452832831((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_14, NULL);
		NullCheck((LowLevelList_1_tD1EA453996325637EAF8C2A867D4DED46603298F*)L_13);
		LowLevelList_1_AddRange_mD5D6366F78D1A613E03AE6A45654DE4C2A6C51BE((LowLevelList_1_tD1EA453996325637EAF8C2A867D4DED46603298F*)L_13, (RuntimeObject*)L_15, LowLevelList_1_AddRange_mD5D6366F78D1A613E03AE6A45654DE4C2A6C51BE_RuntimeMethod_var);
		goto IL_0080;
	}

IL_0060:
	{
		Task_1_tDF1FF540D7D2248A08580387A39717B7FB7A9CF9* L_16 = V_4;
		NullCheck((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_16);
		bool L_17;
		L_17 = Task_get_IsCanceled_m96A8D3F85158A9CB3AEA50A00A55BE4E0F0E21FA((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_16, NULL);
		if (!L_17)
		{
			goto IL_0071;
		}
	}
	{
		Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* L_18 = V_2;
		if (L_18)
		{
			goto IL_0080;
		}
	}
	{
		Task_1_tDF1FF540D7D2248A08580387A39717B7FB7A9CF9* L_19 = V_4;
		V_2 = (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_19;
		goto IL_0080;
	}

IL_0071:
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_20 = V_0;
		int32_t L_21 = V_3;
		Task_1_tDF1FF540D7D2248A08580387A39717B7FB7A9CF9* L_22 = V_4;
		NullCheck(L_22);
		InvokerActionInvoker2< bool, Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 7)), il2cpp_rgctx_method(method->klass->rgctx_data, 7), L_22, (bool)0, (Il2CppFullySharedGenericAny*)L_23);
		NullCheck(L_20);
		il2cpp_codegen_memcpy((L_20)->GetAddressAt(static_cast<il2cpp_array_size_t>(L_21)), L_23, SizeOf_T_tE4830CEB11D3DDB28FE7E64F22C7869BD26E6FCF);
		Il2CppCodeGenWriteBarrierForClass(il2cpp_rgctx_data(method->klass->rgctx_data, 8), (void**)(L_20)->GetAddressAt(static_cast<il2cpp_array_size_t>(L_21)), (void*)L_23);
	}

IL_0080:
	{
		Task_1_tDF1FF540D7D2248A08580387A39717B7FB7A9CF9* L_24 = V_4;
		NullCheck((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_24);
		bool L_25;
		L_25 = Task_get_IsWaitNotificationEnabled_mF6950E2B28561EE2E57DECADAD63B485CA5DD3A8((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)L_24, NULL);
		if (!L_25)
		{
			goto IL_0092;
		}
	}
	{
		NullCheck((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this);
		Task_SetNotificationForWaitCompletion_m6B087B3B1E1B6911006874042808D4D7D9678AED((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this, (bool)1, NULL);
		goto IL_009b;
	}

IL_0092:
	{
		Task_1U5BU5D_t46133A6F873A9EC9FC75DAB1315ABC8025565F09* L_26 = (Task_1U5BU5D_t46133A6F873A9EC9FC75DAB1315ABC8025565F09*)__this->___m_tasks_23;
		int32_t L_27 = V_3;
		NullCheck(L_26);
		ArrayElementTypeCheck (L_26, NULL);
		(L_26)->SetAt(static_cast<il2cpp_array_size_t>(L_27), (Task_1_tDF1FF540D7D2248A08580387A39717B7FB7A9CF9*)NULL);
	}

IL_009b:
	{
		int32_t L_28 = V_3;
		V_3 = ((int32_t)il2cpp_codegen_add(L_28, 1));
	}

IL_009f:
	{
		int32_t L_29 = V_3;
		Task_1U5BU5D_t46133A6F873A9EC9FC75DAB1315ABC8025565F09* L_30 = (Task_1U5BU5D_t46133A6F873A9EC9FC75DAB1315ABC8025565F09*)__this->___m_tasks_23;
		NullCheck(L_30);
		if ((((int32_t)L_29) < ((int32_t)((int32_t)(((RuntimeArray*)L_30)->max_length)))))
		{
			goto IL_0035;
		}
	}
	{
		LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4* L_31 = V_1;
		if (!L_31)
		{
			goto IL_00b6;
		}
	}
	{
		LowLevelListWithIList_1_t77340DBB02E37ACEFB5CF0857C636C82B9F9EED4* L_32 = V_1;
		NullCheck((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this);
		bool L_33;
		L_33 = Task_TrySetException_m8336BA31D11EA84916A89EB8A7A0044D2D0EE94D((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this, (RuntimeObject*)L_32, NULL);
		return;
	}

IL_00b6:
	{
		Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* L_34 = V_2;
		if (!L_34)
		{
			goto IL_00cd;
		}
	}
	{
		Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* L_35 = V_2;
		NullCheck(L_35);
		CancellationToken_t51142D9C6D7C02D314DA34A6A7988C528992FFED L_36;
		L_36 = Task_get_CancellationToken_m459E6E4311018E389AC44E089CCB4ACDC252766A(L_35, NULL);
		Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* L_37 = V_2;
		NullCheck(L_37);
		ExceptionDispatchInfo_tD7AF19E75FEC22F4A8329FD1E9EDF96615CB2757* L_38;
		L_38 = Task_GetCancellationExceptionDispatchInfo_m190A98B306C8BCCB67F3D8B2E7B8BF75EAE63E34(L_37, NULL);
		NullCheck((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this);
		bool L_39;
		L_39 = Task_TrySetCanceled_m8E24757A8DD3AE5A856B64D87B447E08395A0771((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this, L_36, (RuntimeObject*)L_38, NULL);
		return;
	}

IL_00cd:
	{
		il2cpp_codegen_runtime_class_init_inline(DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var);
		bool L_40;
		L_40 = DebuggerSupport_get_LoggingOn_mD838646A5A048C62BAB034257EF0F2F852AF0ABB(NULL);
		if (!L_40)
		{
			goto IL_00dc;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var);
		DebuggerSupport_TraceOperationCompletion_m7047A96BCB7DC4835B38D1B965B4BC3049AF62CB((int32_t)0, (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this, (int32_t)1, NULL);
	}

IL_00dc:
	{
		il2cpp_codegen_runtime_class_init_inline(DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var);
		DebuggerSupport_RemoveFromActiveTasks_m19229D30DAA2447DDAB524F2A0E9718D070A1251_inline((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this, NULL);
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_41 = V_0;
		NullCheck((Task_1_tEA323ABE4B135A0CC68B2D7C3498CC3DA6B8D802*)__this);
		bool L_42;
		L_42 = InvokerFuncInvoker1< bool, __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 9)), il2cpp_rgctx_method(method->klass->rgctx_data, 9), (Task_1_tEA323ABE4B135A0CC68B2D7C3498CC3DA6B8D802*)__this, L_41);
	}

IL_00ea:
	{
		return;
	}
}
// System.Boolean System.Threading.Tasks.Task/WhenAllPromise`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::get_ShouldNotifyDebuggerOfWaitCompletion()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool WhenAllPromise_1_get_ShouldNotifyDebuggerOfWaitCompletion_mEBBC8251BCFB69D90E006DC2E5C4EFC0D45A05FB_gshared (WhenAllPromise_1_tF47371F9AE700EE2F63999402265300771AAFDC3* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	TaskU5BU5D_t368E447BD9A179BA9A26BAAABF1BAE9CA79F60B3* V_0 = NULL;
	{
		NullCheck((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this);
		bool L_0;
		L_0 = Task_get_ShouldNotifyDebuggerOfWaitCompletion_m7AB76993855A98D7FA90003FBACF579C7D7E4534((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572*)__this, NULL);
		if (!L_0)
		{
			goto IL_0016;
		}
	}
	{
		Task_1U5BU5D_t46133A6F873A9EC9FC75DAB1315ABC8025565F09* L_1 = (Task_1U5BU5D_t46133A6F873A9EC9FC75DAB1315ABC8025565F09*)__this->___m_tasks_23;
		V_0 = (TaskU5BU5D_t368E447BD9A179BA9A26BAAABF1BAE9CA79F60B3*)L_1;
		TaskU5BU5D_t368E447BD9A179BA9A26BAAABF1BAE9CA79F60B3* L_2 = V_0;
		il2cpp_codegen_runtime_class_init_inline(Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572_il2cpp_TypeInfo_var);
		bool L_3;
		L_3 = Task_AnyTaskRequiresNotifyDebuggerOfWaitCompletion_m90C692B044AB99E76F7BF6A2BCD909DCDD01FBA3(L_2, NULL);
		return L_3;
	}

IL_0016:
	{
		return (bool)0;
	}
}
// System.Boolean System.Threading.Tasks.Task/WhenAllPromise`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::get_InvokeMayRunArbitraryCode()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool WhenAllPromise_1_get_InvokeMayRunArbitraryCode_m6250FAC7B2F407DA8142F20274EA3277CF3BBB0C_gshared (WhenAllPromise_1_tF47371F9AE700EE2F63999402265300771AAFDC3* __this, const RuntimeMethod* method) 
{
	{
		return (bool)1;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void System.Linq.Enumerable/WhereArrayIterator`1<System.Object>::.ctor(TSource[],System.Func`2<TSource,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereArrayIterator_1__ctor_mC17BAA23BA92C7455512FDA5B1618C6D2B54ACA2_gshared (WhereArrayIterator_1_t027D2511F9B69346688FE3E5623EF2BEE81E9FAA* __this, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___source0, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate1, const RuntimeMethod* method) 
{
	{
		Iterator_1__ctor_m3E47867714E05673E54C6B73D9242F5FFADA1F63((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 0));
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_0 = ___source0;
		__this->___source_3 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___source_3), (void*)L_0);
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_1 = ___predicate1;
		__this->___predicate_4 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___predicate_4), (void*)L_1);
		return;
	}
}
// System.Linq.Enumerable/Iterator`1<TSource> System.Linq.Enumerable/WhereArrayIterator`1<System.Object>::Clone()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA* WhereArrayIterator_1_Clone_m23B21F0E17F85746DFAF09C90772262DF3B707AF_gshared (WhereArrayIterator_1_t027D2511F9B69346688FE3E5623EF2BEE81E9FAA* __this, const RuntimeMethod* method) 
{
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_0 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)__this->___source_3;
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_1 = (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*)__this->___predicate_4;
		WhereArrayIterator_1_t027D2511F9B69346688FE3E5623EF2BEE81E9FAA* L_2 = (WhereArrayIterator_1_t027D2511F9B69346688FE3E5623EF2BEE81E9FAA*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 3));
		NullCheck(L_2);
		WhereArrayIterator_1__ctor_mC17BAA23BA92C7455512FDA5B1618C6D2B54ACA2(L_2, L_0, L_1, il2cpp_rgctx_method(method->klass->rgctx_data, 5));
		return (Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)L_2;
	}
}
// System.Boolean System.Linq.Enumerable/WhereArrayIterator`1<System.Object>::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool WhereArrayIterator_1_MoveNext_m205D669337F73902F61F7BBFD6165B9005890564_gshared (WhereArrayIterator_1_t027D2511F9B69346688FE3E5623EF2BEE81E9FAA* __this, const RuntimeMethod* method) 
{
	RuntimeObject* V_0 = NULL;
	{
		int32_t L_0 = (int32_t)((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this)->___state_1;
		if ((!(((uint32_t)L_0) == ((uint32_t)1))))
		{
			goto IL_0058;
		}
	}
	{
		goto IL_0042;
	}

IL_000b:
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)__this->___source_3;
		int32_t L_2 = (int32_t)__this->___index_5;
		NullCheck(L_1);
		int32_t L_3 = L_2;
		RuntimeObject* L_4 = (L_1)->GetAt(static_cast<il2cpp_array_size_t>(L_3));
		V_0 = L_4;
		int32_t L_5 = (int32_t)__this->___index_5;
		__this->___index_5 = ((int32_t)il2cpp_codegen_add(L_5, 1));
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_6 = (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*)__this->___predicate_4;
		RuntimeObject* L_7 = V_0;
		NullCheck(L_6);
		bool L_8;
		L_8 = Func_2_Invoke_m2014423FB900F135C8FF994125604FF9E6AAE829_inline(L_6, L_7, il2cpp_rgctx_method(method->klass->rgctx_data, 8));
		if (!L_8)
		{
			goto IL_0042;
		}
	}
	{
		RuntimeObject* L_9 = V_0;
		((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this)->___current_2 = L_9;
		Il2CppCodeGenWriteBarrier((void**)(&((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this)->___current_2), (void*)L_9);
		return (bool)1;
	}

IL_0042:
	{
		int32_t L_10 = (int32_t)__this->___index_5;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_11 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)__this->___source_3;
		NullCheck(L_11);
		if ((((int32_t)L_10) < ((int32_t)((int32_t)(((RuntimeArray*)L_11)->max_length)))))
		{
			goto IL_000b;
		}
	}
	{
		NullCheck((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this);
		VirtualActionInvoker0::Invoke(12 /* System.Void System.Linq.Enumerable/Iterator`1<System.Object>::Dispose() */, (Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this);
	}

IL_0058:
	{
		return (bool)0;
	}
}
// System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable/WhereArrayIterator`1<System.Object>::Where(System.Func`2<TSource,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* WhereArrayIterator_1_Where_m00D679C5996A876F2AF50976C1F93D89F8F42C62_gshared (WhereArrayIterator_1_t027D2511F9B69346688FE3E5623EF2BEE81E9FAA* __this, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate0, const RuntimeMethod* method) 
{
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_0 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)__this->___source_3;
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_1 = (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*)__this->___predicate_4;
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_2 = ___predicate0;
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_3;
		L_3 = Enumerable_CombinePredicates_TisRuntimeObject_m613479C29B013E8FC2987E22F42A3BC6CC2C9768(L_1, L_2, il2cpp_rgctx_method(method->klass->rgctx_data, 10));
		WhereArrayIterator_1_t027D2511F9B69346688FE3E5623EF2BEE81E9FAA* L_4 = (WhereArrayIterator_1_t027D2511F9B69346688FE3E5623EF2BEE81E9FAA*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 3));
		NullCheck(L_4);
		WhereArrayIterator_1__ctor_mC17BAA23BA92C7455512FDA5B1618C6D2B54ACA2(L_4, L_0, L_3, il2cpp_rgctx_method(method->klass->rgctx_data, 5));
		return (RuntimeObject*)L_4;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void System.Linq.Enumerable/WhereArrayIterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::.ctor(TSource[],System.Func`2<TSource,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereArrayIterator_1__ctor_mD8BDE04F9897AAED299EE4DC32BF3879F2CBB668_gshared (/*System.Linq.Enumerable/WhereArrayIterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>*/WhereArrayIterator_1_tA7187088CE8DF4724576F6B7F633203C144505F6* __this, __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* ___source0, Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* ___predicate1, const RuntimeMethod* method) 
{
	{
		InvokerActionInvoker0::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 0)), il2cpp_rgctx_method(method->klass->rgctx_data, 0), (Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0*)__this);
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_0 = ___source0;
		il2cpp_codegen_write_instance_field_data<__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC*>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),0), L_0);
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_1 = ___predicate1;
		il2cpp_codegen_write_instance_field_data<Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B*>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),1), L_1);
		return;
	}
}
// System.Linq.Enumerable/Iterator`1<TSource> System.Linq.Enumerable/WhereArrayIterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::Clone()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0* WhereArrayIterator_1_Clone_m1D80001794E47D2DF00A77273FD71D61987E8A44_gshared (/*System.Linq.Enumerable/WhereArrayIterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>*/WhereArrayIterator_1_tA7187088CE8DF4724576F6B7F633203C144505F6* __this, const RuntimeMethod* method) 
{
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_0 = *(__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),0));
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_1 = *(Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),1));
		WhereArrayIterator_1_tA7187088CE8DF4724576F6B7F633203C144505F6* L_2 = (WhereArrayIterator_1_tA7187088CE8DF4724576F6B7F633203C144505F6*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 3));
		NullCheck(L_2);
		InvokerActionInvoker2< __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC*, Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 5)), il2cpp_rgctx_method(method->klass->rgctx_data, 5), L_2, L_0, L_1);
		return (Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0*)L_2;
	}
}
// System.Boolean System.Linq.Enumerable/WhereArrayIterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool WhereArrayIterator_1_MoveNext_m42FC055181A1CDD12BBB46A9EE9ED76C6048BA07_gshared (/*System.Linq.Enumerable/WhereArrayIterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>*/WhereArrayIterator_1_tA7187088CE8DF4724576F6B7F633203C144505F6* __this, const RuntimeMethod* method) 
{
	// sizeof(TSource)
	const uint32_t SizeOf_TSource_tA44A3A99F6F77148305A3C32D2C4DE1D4226338A = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->klass->rgctx_data, 7));
	// TSource
	const Il2CppFullySharedGenericAny L_4 = alloca(SizeOf_TSource_tA44A3A99F6F77148305A3C32D2C4DE1D4226338A);
	const Il2CppFullySharedGenericAny L_9 = L_4;
	const Il2CppFullySharedGenericAny L_7 = alloca(SizeOf_TSource_tA44A3A99F6F77148305A3C32D2C4DE1D4226338A);
	Il2CppFullySharedGenericAny V_0 = alloca(SizeOf_TSource_tA44A3A99F6F77148305A3C32D2C4DE1D4226338A);
	memset(V_0, 0, SizeOf_TSource_tA44A3A99F6F77148305A3C32D2C4DE1D4226338A);
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 6),1));
		if ((!(((uint32_t)L_0) == ((uint32_t)1))))
		{
			goto IL_0058;
		}
	}
	{
		goto IL_0042;
	}

IL_000b:
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_1 = *(__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),0));
		int32_t L_2 = *(int32_t*)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),2));
		NullCheck(L_1);
		int32_t L_3 = L_2;
		il2cpp_codegen_memcpy(L_4, (L_1)->GetAddressAt(static_cast<il2cpp_array_size_t>(L_3)), SizeOf_TSource_tA44A3A99F6F77148305A3C32D2C4DE1D4226338A);
		il2cpp_codegen_memcpy(V_0, L_4, SizeOf_TSource_tA44A3A99F6F77148305A3C32D2C4DE1D4226338A);
		int32_t L_5 = *(int32_t*)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),2));
		il2cpp_codegen_write_instance_field_data<int32_t>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),2), ((int32_t)il2cpp_codegen_add(L_5, 1)));
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_6 = *(Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),1));
		il2cpp_codegen_memcpy(L_7, V_0, SizeOf_TSource_tA44A3A99F6F77148305A3C32D2C4DE1D4226338A);
		NullCheck(L_6);
		bool L_8;
		L_8 = InvokerFuncInvoker1< bool, Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 8)), il2cpp_rgctx_method(method->klass->rgctx_data, 8), L_6, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->klass->rgctx_data, 7)) ? L_7: *(void**)L_7));
		if (!L_8)
		{
			goto IL_0042;
		}
	}
	{
		il2cpp_codegen_memcpy(L_9, V_0, SizeOf_TSource_tA44A3A99F6F77148305A3C32D2C4DE1D4226338A);
		il2cpp_codegen_write_instance_field_data(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 6),2), L_9, SizeOf_TSource_tA44A3A99F6F77148305A3C32D2C4DE1D4226338A);
		return (bool)1;
	}

IL_0042:
	{
		int32_t L_10 = *(int32_t*)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),2));
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_11 = *(__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),0));
		NullCheck(L_11);
		if ((((int32_t)L_10) < ((int32_t)((int32_t)(((RuntimeArray*)L_11)->max_length)))))
		{
			goto IL_000b;
		}
	}
	{
		NullCheck((Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0*)__this);
		VirtualActionInvoker0Invoker::Invoke(12 /* System.Void System.Linq.Enumerable/Iterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::Dispose() */, (Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0*)__this);
	}

IL_0058:
	{
		return (bool)0;
	}
}
// System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable/WhereArrayIterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::Where(System.Func`2<TSource,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* WhereArrayIterator_1_Where_mB2C59E78355E518D359A6D5035BCD6254337B84E_gshared (/*System.Linq.Enumerable/WhereArrayIterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>*/WhereArrayIterator_1_tA7187088CE8DF4724576F6B7F633203C144505F6* __this, Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* ___predicate0, const RuntimeMethod* method) 
{
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_0 = *(__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),0));
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_1 = *(Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),1));
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_2 = ___predicate0;
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_3;
		L_3 = InvokerFuncInvoker2< Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B*, Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B*, Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 10)), il2cpp_rgctx_method(method->klass->rgctx_data, 10), NULL, L_1, L_2);
		WhereArrayIterator_1_tA7187088CE8DF4724576F6B7F633203C144505F6* L_4 = (WhereArrayIterator_1_tA7187088CE8DF4724576F6B7F633203C144505F6*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 3));
		NullCheck(L_4);
		InvokerActionInvoker2< __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC*, Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 5)), il2cpp_rgctx_method(method->klass->rgctx_data, 5), L_4, L_0, L_3);
		return (RuntimeObject*)L_4;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void System.Linq.Enumerable/WhereEnumerableIterator`1<System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::.ctor(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereEnumerableIterator_1__ctor_mC87BD639674AE32961C0AC5A5878F4144E7DA780_gshared (WhereEnumerableIterator_1_tB576B4EE76F70B81DDF11FA35260EC589825E7C5* __this, RuntimeObject* ___source0, Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B* ___predicate1, const RuntimeMethod* method) 
{
	{
		Iterator_1__ctor_m27CEA132FC2E7D7BA6A8529CE0647F2709FC29A3((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 0));
		RuntimeObject* L_0 = ___source0;
		__this->___source_3 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___source_3), (void*)L_0);
		Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B* L_1 = ___predicate1;
		__this->___predicate_4 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___predicate_4), (void*)L_1);
		return;
	}
}
// System.Linq.Enumerable/Iterator`1<TSource> System.Linq.Enumerable/WhereEnumerableIterator`1<System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::Clone()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40* WhereEnumerableIterator_1_Clone_m280CCE04A09CE144E19EFA9F1C7D44ACBAF7F78C_gshared (WhereEnumerableIterator_1_tB576B4EE76F70B81DDF11FA35260EC589825E7C5* __this, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = (RuntimeObject*)__this->___source_3;
		Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B* L_1 = (Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B*)__this->___predicate_4;
		WhereEnumerableIterator_1_tB576B4EE76F70B81DDF11FA35260EC589825E7C5* L_2 = (WhereEnumerableIterator_1_tB576B4EE76F70B81DDF11FA35260EC589825E7C5*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 3));
		NullCheck(L_2);
		WhereEnumerableIterator_1__ctor_mC87BD639674AE32961C0AC5A5878F4144E7DA780(L_2, L_0, L_1, il2cpp_rgctx_method(method->klass->rgctx_data, 5));
		return (Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)L_2;
	}
}
// System.Void System.Linq.Enumerable/WhereEnumerableIterator`1<System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::Dispose()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereEnumerableIterator_1_Dispose_m64279721BEA3708662EB397E2A1219397DBA4846_gshared (WhereEnumerableIterator_1_tB576B4EE76F70B81DDF11FA35260EC589825E7C5* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		RuntimeObject* L_0 = (RuntimeObject*)__this->___enumerator_5;
		if (!L_0)
		{
			goto IL_0013;
		}
	}
	{
		RuntimeObject* L_1 = (RuntimeObject*)__this->___enumerator_5;
		NullCheck((RuntimeObject*)L_1);
		InterfaceActionInvoker0::Invoke(0 /* System.Void System.IDisposable::Dispose() */, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, (RuntimeObject*)L_1);
	}

IL_0013:
	{
		__this->___enumerator_5 = (RuntimeObject*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___enumerator_5), (void*)(RuntimeObject*)NULL);
		NullCheck((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this);
		Iterator_1_Dispose_m51BD0D9CCB4CB032C42DA20F3C867462771689AB((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 8));
		return;
	}
}
// System.Boolean System.Linq.Enumerable/WhereEnumerableIterator`1<System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool WhereEnumerableIterator_1_MoveNext_m455076760D8FB86E47BF0E549906836276711EC3_gshared (WhereEnumerableIterator_1_tB576B4EE76F70B81DDF11FA35260EC589825E7C5* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	ValueTuple_2_t761B36235D5F97F497A1CBCD077DF3858814FADE V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		int32_t L_0 = (int32_t)((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this)->___state_1;
		V_0 = L_0;
		int32_t L_1 = V_0;
		if ((((int32_t)L_1) == ((int32_t)1)))
		{
			goto IL_0011;
		}
	}
	{
		int32_t L_2 = V_0;
		if ((((int32_t)L_2) == ((int32_t)2)))
		{
			goto IL_004e;
		}
	}
	{
		goto IL_0061;
	}

IL_0011:
	{
		RuntimeObject* L_3 = (RuntimeObject*)__this->___source_3;
		NullCheck(L_3);
		RuntimeObject* L_4;
		L_4 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0 /* System.Collections.Generic.IEnumerator`1<T> System.Collections.Generic.IEnumerable`1<System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::GetEnumerator() */, il2cpp_rgctx_data(method->klass->rgctx_data, 2), L_3);
		__this->___enumerator_5 = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___enumerator_5), (void*)L_4);
		((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this)->___state_1 = 2;
		goto IL_004e;
	}

IL_002b:
	{
		RuntimeObject* L_5 = (RuntimeObject*)__this->___enumerator_5;
		NullCheck(L_5);
		ValueTuple_2_t761B36235D5F97F497A1CBCD077DF3858814FADE L_6;
		L_6 = InterfaceFuncInvoker0< ValueTuple_2_t761B36235D5F97F497A1CBCD077DF3858814FADE >::Invoke(0 /* T System.Collections.Generic.IEnumerator`1<System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::get_Current() */, il2cpp_rgctx_data(method->klass->rgctx_data, 7), L_5);
		V_1 = L_6;
		Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B* L_7 = (Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B*)__this->___predicate_4;
		ValueTuple_2_t761B36235D5F97F497A1CBCD077DF3858814FADE L_8 = V_1;
		NullCheck(L_7);
		bool L_9;
		L_9 = Func_2_Invoke_m1DC9E7C418A082CE71CE5A7F12B8F8F30528804F_inline(L_7, L_8, il2cpp_rgctx_method(method->klass->rgctx_data, 12));
		if (!L_9)
		{
			goto IL_004e;
		}
	}
	{
		ValueTuple_2_t761B36235D5F97F497A1CBCD077DF3858814FADE L_10 = V_1;
		((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this)->___current_2 = L_10;
		Il2CppCodeGenWriteBarrier((void**)&(((&((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this)->___current_2))->___Item1_0), (void*)NULL);
		return (bool)1;
	}

IL_004e:
	{
		RuntimeObject* L_11 = (RuntimeObject*)__this->___enumerator_5;
		NullCheck((RuntimeObject*)L_11);
		bool L_12;
		L_12 = InterfaceFuncInvoker0< bool >::Invoke(0 /* System.Boolean System.Collections.IEnumerator::MoveNext() */, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, (RuntimeObject*)L_11);
		if (L_12)
		{
			goto IL_002b;
		}
	}
	{
		NullCheck((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this);
		VirtualActionInvoker0::Invoke(12 /* System.Void System.Linq.Enumerable/Iterator`1<System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::Dispose() */, (Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this);
	}

IL_0061:
	{
		return (bool)0;
	}
}
// System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable/WhereEnumerableIterator`1<System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::Where(System.Func`2<TSource,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* WhereEnumerableIterator_1_Where_m5ED5B4FB7E20757E7A0E3CE28229B87D930BA7B0_gshared (WhereEnumerableIterator_1_tB576B4EE76F70B81DDF11FA35260EC589825E7C5* __this, Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B* ___predicate0, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = (RuntimeObject*)__this->___source_3;
		Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B* L_1 = (Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B*)__this->___predicate_4;
		Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B* L_2 = ___predicate0;
		Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B* L_3;
		L_3 = Enumerable_CombinePredicates_TisValueTuple_2_t761B36235D5F97F497A1CBCD077DF3858814FADE_m97FA1ECA0B2F0FA21E3CF45AAEB7C5157CC47603(L_1, L_2, il2cpp_rgctx_method(method->klass->rgctx_data, 13));
		WhereEnumerableIterator_1_tB576B4EE76F70B81DDF11FA35260EC589825E7C5* L_4 = (WhereEnumerableIterator_1_tB576B4EE76F70B81DDF11FA35260EC589825E7C5*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 3));
		NullCheck(L_4);
		WhereEnumerableIterator_1__ctor_mC87BD639674AE32961C0AC5A5878F4144E7DA780(L_4, L_0, L_3, il2cpp_rgctx_method(method->klass->rgctx_data, 5));
		return (RuntimeObject*)L_4;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void System.Linq.Enumerable/WhereEnumerableIterator`1<System.Object>::.ctor(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereEnumerableIterator_1__ctor_m8C0DA4CDA5431C03561F67C4393BB18CDD891F01_gshared (WhereEnumerableIterator_1_t1E787D13759F5A31C94B3FAED181402B25C278F4* __this, RuntimeObject* ___source0, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate1, const RuntimeMethod* method) 
{
	{
		Iterator_1__ctor_m3E47867714E05673E54C6B73D9242F5FFADA1F63((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 0));
		RuntimeObject* L_0 = ___source0;
		__this->___source_3 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___source_3), (void*)L_0);
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_1 = ___predicate1;
		__this->___predicate_4 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___predicate_4), (void*)L_1);
		return;
	}
}
// System.Linq.Enumerable/Iterator`1<TSource> System.Linq.Enumerable/WhereEnumerableIterator`1<System.Object>::Clone()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA* WhereEnumerableIterator_1_Clone_m25DBF44FABBFE76AB4314BD7F62334FE2A74F5CA_gshared (WhereEnumerableIterator_1_t1E787D13759F5A31C94B3FAED181402B25C278F4* __this, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = (RuntimeObject*)__this->___source_3;
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_1 = (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*)__this->___predicate_4;
		WhereEnumerableIterator_1_t1E787D13759F5A31C94B3FAED181402B25C278F4* L_2 = (WhereEnumerableIterator_1_t1E787D13759F5A31C94B3FAED181402B25C278F4*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 3));
		NullCheck(L_2);
		WhereEnumerableIterator_1__ctor_m8C0DA4CDA5431C03561F67C4393BB18CDD891F01(L_2, L_0, L_1, il2cpp_rgctx_method(method->klass->rgctx_data, 5));
		return (Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)L_2;
	}
}
// System.Void System.Linq.Enumerable/WhereEnumerableIterator`1<System.Object>::Dispose()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereEnumerableIterator_1_Dispose_m7EB3C00CC0ED06056CF70FE322BF44A93F0C4136_gshared (WhereEnumerableIterator_1_t1E787D13759F5A31C94B3FAED181402B25C278F4* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		RuntimeObject* L_0 = (RuntimeObject*)__this->___enumerator_5;
		if (!L_0)
		{
			goto IL_0013;
		}
	}
	{
		RuntimeObject* L_1 = (RuntimeObject*)__this->___enumerator_5;
		NullCheck((RuntimeObject*)L_1);
		InterfaceActionInvoker0::Invoke(0 /* System.Void System.IDisposable::Dispose() */, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, (RuntimeObject*)L_1);
	}

IL_0013:
	{
		__this->___enumerator_5 = (RuntimeObject*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___enumerator_5), (void*)(RuntimeObject*)NULL);
		NullCheck((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this);
		Iterator_1_Dispose_m953BCF886C8A63456821023DBA45EBD9AC44EB07((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 8));
		return;
	}
}
// System.Boolean System.Linq.Enumerable/WhereEnumerableIterator`1<System.Object>::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool WhereEnumerableIterator_1_MoveNext_m7F8C3A8E4FC2835971FF35C1F4C51A061483BEDD_gshared (WhereEnumerableIterator_1_t1E787D13759F5A31C94B3FAED181402B25C278F4* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	RuntimeObject* V_1 = NULL;
	{
		int32_t L_0 = (int32_t)((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this)->___state_1;
		V_0 = L_0;
		int32_t L_1 = V_0;
		if ((((int32_t)L_1) == ((int32_t)1)))
		{
			goto IL_0011;
		}
	}
	{
		int32_t L_2 = V_0;
		if ((((int32_t)L_2) == ((int32_t)2)))
		{
			goto IL_004e;
		}
	}
	{
		goto IL_0061;
	}

IL_0011:
	{
		RuntimeObject* L_3 = (RuntimeObject*)__this->___source_3;
		NullCheck(L_3);
		RuntimeObject* L_4;
		L_4 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0 /* System.Collections.Generic.IEnumerator`1<T> System.Collections.Generic.IEnumerable`1<System.Object>::GetEnumerator() */, il2cpp_rgctx_data(method->klass->rgctx_data, 2), L_3);
		__this->___enumerator_5 = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___enumerator_5), (void*)L_4);
		((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this)->___state_1 = 2;
		goto IL_004e;
	}

IL_002b:
	{
		RuntimeObject* L_5 = (RuntimeObject*)__this->___enumerator_5;
		NullCheck(L_5);
		RuntimeObject* L_6;
		L_6 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0 /* T System.Collections.Generic.IEnumerator`1<System.Object>::get_Current() */, il2cpp_rgctx_data(method->klass->rgctx_data, 7), L_5);
		V_1 = L_6;
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_7 = (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*)__this->___predicate_4;
		RuntimeObject* L_8 = V_1;
		NullCheck(L_7);
		bool L_9;
		L_9 = Func_2_Invoke_m2014423FB900F135C8FF994125604FF9E6AAE829_inline(L_7, L_8, il2cpp_rgctx_method(method->klass->rgctx_data, 12));
		if (!L_9)
		{
			goto IL_004e;
		}
	}
	{
		RuntimeObject* L_10 = V_1;
		((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this)->___current_2 = L_10;
		Il2CppCodeGenWriteBarrier((void**)(&((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this)->___current_2), (void*)L_10);
		return (bool)1;
	}

IL_004e:
	{
		RuntimeObject* L_11 = (RuntimeObject*)__this->___enumerator_5;
		NullCheck((RuntimeObject*)L_11);
		bool L_12;
		L_12 = InterfaceFuncInvoker0< bool >::Invoke(0 /* System.Boolean System.Collections.IEnumerator::MoveNext() */, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, (RuntimeObject*)L_11);
		if (L_12)
		{
			goto IL_002b;
		}
	}
	{
		NullCheck((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this);
		VirtualActionInvoker0::Invoke(12 /* System.Void System.Linq.Enumerable/Iterator`1<System.Object>::Dispose() */, (Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this);
	}

IL_0061:
	{
		return (bool)0;
	}
}
// System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable/WhereEnumerableIterator`1<System.Object>::Where(System.Func`2<TSource,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* WhereEnumerableIterator_1_Where_m48ED4EDDA686615E779F1400A17479B243C85100_gshared (WhereEnumerableIterator_1_t1E787D13759F5A31C94B3FAED181402B25C278F4* __this, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate0, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = (RuntimeObject*)__this->___source_3;
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_1 = (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*)__this->___predicate_4;
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_2 = ___predicate0;
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_3;
		L_3 = Enumerable_CombinePredicates_TisRuntimeObject_m613479C29B013E8FC2987E22F42A3BC6CC2C9768(L_1, L_2, il2cpp_rgctx_method(method->klass->rgctx_data, 13));
		WhereEnumerableIterator_1_t1E787D13759F5A31C94B3FAED181402B25C278F4* L_4 = (WhereEnumerableIterator_1_t1E787D13759F5A31C94B3FAED181402B25C278F4*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 3));
		NullCheck(L_4);
		WhereEnumerableIterator_1__ctor_m8C0DA4CDA5431C03561F67C4393BB18CDD891F01(L_4, L_0, L_3, il2cpp_rgctx_method(method->klass->rgctx_data, 5));
		return (RuntimeObject*)L_4;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void System.Linq.Enumerable/WhereEnumerableIterator`1<System.Single>::.ctor(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereEnumerableIterator_1__ctor_mCB49A03958EF827EF6CE47402259941EAB31D984_gshared (WhereEnumerableIterator_1_t7BB2D1D9F8A6E52243A87F45DFEEA4209D685F7C* __this, RuntimeObject* ___source0, Func_2_t49E998685259ADE759F9329BF66F20DE8667006E* ___predicate1, const RuntimeMethod* method) 
{
	{
		Iterator_1__ctor_m7FEBAE985CDE5DD1CB255EB4A3D65F05AA8D2AEF((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 0));
		RuntimeObject* L_0 = ___source0;
		__this->___source_3 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___source_3), (void*)L_0);
		Func_2_t49E998685259ADE759F9329BF66F20DE8667006E* L_1 = ___predicate1;
		__this->___predicate_4 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___predicate_4), (void*)L_1);
		return;
	}
}
// System.Linq.Enumerable/Iterator`1<TSource> System.Linq.Enumerable/WhereEnumerableIterator`1<System.Single>::Clone()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E* WhereEnumerableIterator_1_Clone_m68ABE7D6279F0A7FF07F6207FADAC4A7598D2736_gshared (WhereEnumerableIterator_1_t7BB2D1D9F8A6E52243A87F45DFEEA4209D685F7C* __this, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = (RuntimeObject*)__this->___source_3;
		Func_2_t49E998685259ADE759F9329BF66F20DE8667006E* L_1 = (Func_2_t49E998685259ADE759F9329BF66F20DE8667006E*)__this->___predicate_4;
		WhereEnumerableIterator_1_t7BB2D1D9F8A6E52243A87F45DFEEA4209D685F7C* L_2 = (WhereEnumerableIterator_1_t7BB2D1D9F8A6E52243A87F45DFEEA4209D685F7C*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 3));
		NullCheck(L_2);
		WhereEnumerableIterator_1__ctor_mCB49A03958EF827EF6CE47402259941EAB31D984(L_2, L_0, L_1, il2cpp_rgctx_method(method->klass->rgctx_data, 5));
		return (Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)L_2;
	}
}
// System.Void System.Linq.Enumerable/WhereEnumerableIterator`1<System.Single>::Dispose()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereEnumerableIterator_1_Dispose_mDA783CEC1DAB7DFA6CC0FC62828C8C47575E0C57_gshared (WhereEnumerableIterator_1_t7BB2D1D9F8A6E52243A87F45DFEEA4209D685F7C* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		RuntimeObject* L_0 = (RuntimeObject*)__this->___enumerator_5;
		if (!L_0)
		{
			goto IL_0013;
		}
	}
	{
		RuntimeObject* L_1 = (RuntimeObject*)__this->___enumerator_5;
		NullCheck((RuntimeObject*)L_1);
		InterfaceActionInvoker0::Invoke(0 /* System.Void System.IDisposable::Dispose() */, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, (RuntimeObject*)L_1);
	}

IL_0013:
	{
		__this->___enumerator_5 = (RuntimeObject*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___enumerator_5), (void*)(RuntimeObject*)NULL);
		NullCheck((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this);
		Iterator_1_Dispose_mC0A88E7EEED5A9CFDCACF160DE8F58A713DFF222((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 8));
		return;
	}
}
// System.Boolean System.Linq.Enumerable/WhereEnumerableIterator`1<System.Single>::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool WhereEnumerableIterator_1_MoveNext_mAC1004B10B0C1548B8F5A30EC0DA7E8FE8F5BB35_gshared (WhereEnumerableIterator_1_t7BB2D1D9F8A6E52243A87F45DFEEA4209D685F7C* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	float V_1 = 0.0f;
	{
		int32_t L_0 = (int32_t)((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this)->___state_1;
		V_0 = L_0;
		int32_t L_1 = V_0;
		if ((((int32_t)L_1) == ((int32_t)1)))
		{
			goto IL_0011;
		}
	}
	{
		int32_t L_2 = V_0;
		if ((((int32_t)L_2) == ((int32_t)2)))
		{
			goto IL_004e;
		}
	}
	{
		goto IL_0061;
	}

IL_0011:
	{
		RuntimeObject* L_3 = (RuntimeObject*)__this->___source_3;
		NullCheck(L_3);
		RuntimeObject* L_4;
		L_4 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0 /* System.Collections.Generic.IEnumerator`1<T> System.Collections.Generic.IEnumerable`1<System.Single>::GetEnumerator() */, il2cpp_rgctx_data(method->klass->rgctx_data, 2), L_3);
		__this->___enumerator_5 = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___enumerator_5), (void*)L_4);
		((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this)->___state_1 = 2;
		goto IL_004e;
	}

IL_002b:
	{
		RuntimeObject* L_5 = (RuntimeObject*)__this->___enumerator_5;
		NullCheck(L_5);
		float L_6;
		L_6 = InterfaceFuncInvoker0< float >::Invoke(0 /* T System.Collections.Generic.IEnumerator`1<System.Single>::get_Current() */, il2cpp_rgctx_data(method->klass->rgctx_data, 7), L_5);
		V_1 = L_6;
		Func_2_t49E998685259ADE759F9329BF66F20DE8667006E* L_7 = (Func_2_t49E998685259ADE759F9329BF66F20DE8667006E*)__this->___predicate_4;
		float L_8 = V_1;
		NullCheck(L_7);
		bool L_9;
		L_9 = Func_2_Invoke_m1FE6F2A4EC23CC595897C55AE7B0BDA8969044D7_inline(L_7, L_8, il2cpp_rgctx_method(method->klass->rgctx_data, 12));
		if (!L_9)
		{
			goto IL_004e;
		}
	}
	{
		float L_10 = V_1;
		((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this)->___current_2 = L_10;
		return (bool)1;
	}

IL_004e:
	{
		RuntimeObject* L_11 = (RuntimeObject*)__this->___enumerator_5;
		NullCheck((RuntimeObject*)L_11);
		bool L_12;
		L_12 = InterfaceFuncInvoker0< bool >::Invoke(0 /* System.Boolean System.Collections.IEnumerator::MoveNext() */, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, (RuntimeObject*)L_11);
		if (L_12)
		{
			goto IL_002b;
		}
	}
	{
		NullCheck((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this);
		VirtualActionInvoker0::Invoke(12 /* System.Void System.Linq.Enumerable/Iterator`1<System.Single>::Dispose() */, (Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this);
	}

IL_0061:
	{
		return (bool)0;
	}
}
// System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable/WhereEnumerableIterator`1<System.Single>::Where(System.Func`2<TSource,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* WhereEnumerableIterator_1_Where_m2DDCFF03760C433C49CEEE822BEC46016E6E6B62_gshared (WhereEnumerableIterator_1_t7BB2D1D9F8A6E52243A87F45DFEEA4209D685F7C* __this, Func_2_t49E998685259ADE759F9329BF66F20DE8667006E* ___predicate0, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = (RuntimeObject*)__this->___source_3;
		Func_2_t49E998685259ADE759F9329BF66F20DE8667006E* L_1 = (Func_2_t49E998685259ADE759F9329BF66F20DE8667006E*)__this->___predicate_4;
		Func_2_t49E998685259ADE759F9329BF66F20DE8667006E* L_2 = ___predicate0;
		Func_2_t49E998685259ADE759F9329BF66F20DE8667006E* L_3;
		L_3 = Enumerable_CombinePredicates_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_m2F9BC4BEE0498F9B3486BA254B507C1FCE89A763(L_1, L_2, il2cpp_rgctx_method(method->klass->rgctx_data, 13));
		WhereEnumerableIterator_1_t7BB2D1D9F8A6E52243A87F45DFEEA4209D685F7C* L_4 = (WhereEnumerableIterator_1_t7BB2D1D9F8A6E52243A87F45DFEEA4209D685F7C*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 3));
		NullCheck(L_4);
		WhereEnumerableIterator_1__ctor_mCB49A03958EF827EF6CE47402259941EAB31D984(L_4, L_0, L_3, il2cpp_rgctx_method(method->klass->rgctx_data, 5));
		return (RuntimeObject*)L_4;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void System.Linq.Enumerable/WhereEnumerableIterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::.ctor(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereEnumerableIterator_1__ctor_m2DD2BB86C5517EDD8C051BBF8CE38C43D712A8D6_gshared (/*System.Linq.Enumerable/WhereEnumerableIterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>*/WhereEnumerableIterator_1_t8B24528558F527941435C4FE1D046216FE4F277B* __this, RuntimeObject* ___source0, Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* ___predicate1, const RuntimeMethod* method) 
{
	{
		InvokerActionInvoker0::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 0)), il2cpp_rgctx_method(method->klass->rgctx_data, 0), (Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0*)__this);
		RuntimeObject* L_0 = ___source0;
		il2cpp_codegen_write_instance_field_data<RuntimeObject*>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),0), L_0);
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_1 = ___predicate1;
		il2cpp_codegen_write_instance_field_data<Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B*>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),1), L_1);
		return;
	}
}
// System.Linq.Enumerable/Iterator`1<TSource> System.Linq.Enumerable/WhereEnumerableIterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::Clone()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0* WhereEnumerableIterator_1_Clone_m0317D203B88386A9A479C72FA9D62763FD0A91D3_gshared (/*System.Linq.Enumerable/WhereEnumerableIterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>*/WhereEnumerableIterator_1_t8B24528558F527941435C4FE1D046216FE4F277B* __this, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = *(RuntimeObject**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),0));
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_1 = *(Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),1));
		WhereEnumerableIterator_1_t8B24528558F527941435C4FE1D046216FE4F277B* L_2 = (WhereEnumerableIterator_1_t8B24528558F527941435C4FE1D046216FE4F277B*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 3));
		NullCheck(L_2);
		InvokerActionInvoker2< RuntimeObject*, Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 5)), il2cpp_rgctx_method(method->klass->rgctx_data, 5), L_2, L_0, L_1);
		return (Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0*)L_2;
	}
}
// System.Void System.Linq.Enumerable/WhereEnumerableIterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::Dispose()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereEnumerableIterator_1_Dispose_m2583FECFDC8EDFE66C959C7C386F99E287C5763E_gshared (/*System.Linq.Enumerable/WhereEnumerableIterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>*/WhereEnumerableIterator_1_t8B24528558F527941435C4FE1D046216FE4F277B* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		RuntimeObject* L_0 = *(RuntimeObject**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),2));
		if (!L_0)
		{
			goto IL_0013;
		}
	}
	{
		RuntimeObject* L_1 = *(RuntimeObject**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),2));
		NullCheck((RuntimeObject*)L_1);
		InterfaceActionInvoker0::Invoke(0 /* System.Void System.IDisposable::Dispose() */, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, (RuntimeObject*)L_1);
	}

IL_0013:
	{
		il2cpp_codegen_write_instance_field_data<RuntimeObject*>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),2), (RuntimeObject*)NULL);
		NullCheck((Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0*)__this);
		InvokerActionInvoker0::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 8)), il2cpp_rgctx_method(method->klass->rgctx_data, 8), (Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0*)__this);
		return;
	}
}
// System.Boolean System.Linq.Enumerable/WhereEnumerableIterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool WhereEnumerableIterator_1_MoveNext_m1A18D4050C069B6C4310DAB9857281E37DCB2C69_gshared (/*System.Linq.Enumerable/WhereEnumerableIterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>*/WhereEnumerableIterator_1_t8B24528558F527941435C4FE1D046216FE4F277B* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	// sizeof(TSource)
	const uint32_t SizeOf_TSource_tC0EDCBB06D927E5200EDA4B413FCECB2FDD7AFEB = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->klass->rgctx_data, 11));
	// TSource
	const Il2CppFullySharedGenericAny L_6 = alloca(SizeOf_TSource_tC0EDCBB06D927E5200EDA4B413FCECB2FDD7AFEB);
	const Il2CppFullySharedGenericAny L_10 = L_6;
	const Il2CppFullySharedGenericAny L_8 = alloca(SizeOf_TSource_tC0EDCBB06D927E5200EDA4B413FCECB2FDD7AFEB);
	int32_t V_0 = 0;
	Il2CppFullySharedGenericAny V_1 = alloca(SizeOf_TSource_tC0EDCBB06D927E5200EDA4B413FCECB2FDD7AFEB);
	memset(V_1, 0, SizeOf_TSource_tC0EDCBB06D927E5200EDA4B413FCECB2FDD7AFEB);
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 6),1));
		V_0 = L_0;
		int32_t L_1 = V_0;
		if ((((int32_t)L_1) == ((int32_t)1)))
		{
			goto IL_0011;
		}
	}
	{
		int32_t L_2 = V_0;
		if ((((int32_t)L_2) == ((int32_t)2)))
		{
			goto IL_004e;
		}
	}
	{
		goto IL_0061;
	}

IL_0011:
	{
		RuntimeObject* L_3 = *(RuntimeObject**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),0));
		NullCheck(L_3);
		RuntimeObject* L_4;
		L_4 = InterfaceFuncInvoker0Invoker< RuntimeObject* >::Invoke(0 /* System.Collections.Generic.IEnumerator`1<T> System.Collections.Generic.IEnumerable`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::GetEnumerator() */, il2cpp_rgctx_data(method->klass->rgctx_data, 2), L_3);
		il2cpp_codegen_write_instance_field_data<RuntimeObject*>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),2), L_4);
		il2cpp_codegen_write_instance_field_data<int32_t>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 6),1), 2);
		goto IL_004e;
	}

IL_002b:
	{
		RuntimeObject* L_5 = *(RuntimeObject**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),2));
		NullCheck(L_5);
		InterfaceActionInvoker1Invoker< Il2CppFullySharedGenericAny* >::Invoke(0 /* T System.Collections.Generic.IEnumerator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::get_Current() */, il2cpp_rgctx_data(method->klass->rgctx_data, 7), L_5, (Il2CppFullySharedGenericAny*)L_6);
		il2cpp_codegen_memcpy(V_1, L_6, SizeOf_TSource_tC0EDCBB06D927E5200EDA4B413FCECB2FDD7AFEB);
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_7 = *(Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),1));
		il2cpp_codegen_memcpy(L_8, V_1, SizeOf_TSource_tC0EDCBB06D927E5200EDA4B413FCECB2FDD7AFEB);
		NullCheck(L_7);
		bool L_9;
		L_9 = InvokerFuncInvoker1< bool, Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 12)), il2cpp_rgctx_method(method->klass->rgctx_data, 12), L_7, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->klass->rgctx_data, 11)) ? L_8: *(void**)L_8));
		if (!L_9)
		{
			goto IL_004e;
		}
	}
	{
		il2cpp_codegen_memcpy(L_10, V_1, SizeOf_TSource_tC0EDCBB06D927E5200EDA4B413FCECB2FDD7AFEB);
		il2cpp_codegen_write_instance_field_data(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 6),2), L_10, SizeOf_TSource_tC0EDCBB06D927E5200EDA4B413FCECB2FDD7AFEB);
		return (bool)1;
	}

IL_004e:
	{
		RuntimeObject* L_11 = *(RuntimeObject**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),2));
		NullCheck((RuntimeObject*)L_11);
		bool L_12;
		L_12 = InterfaceFuncInvoker0< bool >::Invoke(0 /* System.Boolean System.Collections.IEnumerator::MoveNext() */, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, (RuntimeObject*)L_11);
		if (L_12)
		{
			goto IL_002b;
		}
	}
	{
		NullCheck((Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0*)__this);
		VirtualActionInvoker0Invoker::Invoke(12 /* System.Void System.Linq.Enumerable/Iterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::Dispose() */, (Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0*)__this);
	}

IL_0061:
	{
		return (bool)0;
	}
}
// System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable/WhereEnumerableIterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::Where(System.Func`2<TSource,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* WhereEnumerableIterator_1_Where_mC623267514B4299E409A01161DBBDA5362CEDFC2_gshared (/*System.Linq.Enumerable/WhereEnumerableIterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>*/WhereEnumerableIterator_1_t8B24528558F527941435C4FE1D046216FE4F277B* __this, Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* ___predicate0, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = *(RuntimeObject**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),0));
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_1 = *(Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),1));
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_2 = ___predicate0;
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_3;
		L_3 = InvokerFuncInvoker2< Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B*, Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B*, Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 13)), il2cpp_rgctx_method(method->klass->rgctx_data, 13), NULL, L_1, L_2);
		WhereEnumerableIterator_1_t8B24528558F527941435C4FE1D046216FE4F277B* L_4 = (WhereEnumerableIterator_1_t8B24528558F527941435C4FE1D046216FE4F277B*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 3));
		NullCheck(L_4);
		InvokerActionInvoker2< RuntimeObject*, Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 5)), il2cpp_rgctx_method(method->klass->rgctx_data, 5), L_4, L_0, L_3);
		return (RuntimeObject*)L_4;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void System.Linq.Enumerable/WhereListIterator`1<System.Object>::.ctor(System.Collections.Generic.List`1<TSource>,System.Func`2<TSource,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereListIterator_1__ctor_mEF8F62B9078E538C1DC46BCB876C2AC5B29EA73B_gshared (WhereListIterator_1_t1F40F08BAF8586F2C09294085BC605CC2FA432EB* __this, List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* ___source0, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate1, const RuntimeMethod* method) 
{
	{
		Iterator_1__ctor_m3E47867714E05673E54C6B73D9242F5FFADA1F63((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 0));
		List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* L_0 = ___source0;
		__this->___source_3 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___source_3), (void*)L_0);
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_1 = ___predicate1;
		__this->___predicate_4 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___predicate_4), (void*)L_1);
		return;
	}
}
// System.Linq.Enumerable/Iterator`1<TSource> System.Linq.Enumerable/WhereListIterator`1<System.Object>::Clone()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA* WhereListIterator_1_Clone_mB7087945B135AFA9D70F30479082AD370DDDB66A_gshared (WhereListIterator_1_t1F40F08BAF8586F2C09294085BC605CC2FA432EB* __this, const RuntimeMethod* method) 
{
	{
		List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* L_0 = (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D*)__this->___source_3;
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_1 = (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*)__this->___predicate_4;
		WhereListIterator_1_t1F40F08BAF8586F2C09294085BC605CC2FA432EB* L_2 = (WhereListIterator_1_t1F40F08BAF8586F2C09294085BC605CC2FA432EB*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 3));
		NullCheck(L_2);
		WhereListIterator_1__ctor_mEF8F62B9078E538C1DC46BCB876C2AC5B29EA73B(L_2, L_0, L_1, il2cpp_rgctx_method(method->klass->rgctx_data, 5));
		return (Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)L_2;
	}
}
// System.Boolean System.Linq.Enumerable/WhereListIterator`1<System.Object>::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool WhereListIterator_1_MoveNext_mEE70CAE79424880884D3CD6947167DEDB297FB47_gshared (WhereListIterator_1_t1F40F08BAF8586F2C09294085BC605CC2FA432EB* __this, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	RuntimeObject* V_1 = NULL;
	{
		int32_t L_0 = (int32_t)((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this)->___state_1;
		V_0 = L_0;
		int32_t L_1 = V_0;
		if ((((int32_t)L_1) == ((int32_t)1)))
		{
			goto IL_0011;
		}
	}
	{
		int32_t L_2 = V_0;
		if ((((int32_t)L_2) == ((int32_t)2)))
		{
			goto IL_004e;
		}
	}
	{
		goto IL_0061;
	}

IL_0011:
	{
		List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* L_3 = (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D*)__this->___source_3;
		NullCheck(L_3);
		Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A L_4;
		L_4 = List_1_GetEnumerator_mD8294A7FA2BEB1929487127D476F8EC1CDC23BFC(L_3, il2cpp_rgctx_method(method->klass->rgctx_data, 7));
		__this->___enumerator_5 = L_4;
		Il2CppCodeGenWriteBarrier((void**)&(((&__this->___enumerator_5))->____list_0), (void*)NULL);
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&(((&__this->___enumerator_5))->____current_3), (void*)NULL);
		#endif
		((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this)->___state_1 = 2;
		goto IL_004e;
	}

IL_002b:
	{
		Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A* L_5 = (Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A*)(&__this->___enumerator_5);
		RuntimeObject* L_6;
		L_6 = Enumerator_get_Current_m6330F15D18EE4F547C05DF9BF83C5EB710376027_inline(L_5, il2cpp_rgctx_method(method->klass->rgctx_data, 9));
		V_1 = L_6;
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_7 = (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*)__this->___predicate_4;
		RuntimeObject* L_8 = V_1;
		NullCheck(L_7);
		bool L_9;
		L_9 = Func_2_Invoke_m2014423FB900F135C8FF994125604FF9E6AAE829_inline(L_7, L_8, il2cpp_rgctx_method(method->klass->rgctx_data, 11));
		if (!L_9)
		{
			goto IL_004e;
		}
	}
	{
		RuntimeObject* L_10 = V_1;
		((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this)->___current_2 = L_10;
		Il2CppCodeGenWriteBarrier((void**)(&((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this)->___current_2), (void*)L_10);
		return (bool)1;
	}

IL_004e:
	{
		Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A* L_11 = (Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A*)(&__this->___enumerator_5);
		bool L_12;
		L_12 = Enumerator_MoveNext_mE921CC8F29FBBDE7CC3209A0ED0D921D58D00BCB(L_11, il2cpp_rgctx_method(method->klass->rgctx_data, 12));
		if (L_12)
		{
			goto IL_002b;
		}
	}
	{
		NullCheck((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this);
		VirtualActionInvoker0::Invoke(12 /* System.Void System.Linq.Enumerable/Iterator`1<System.Object>::Dispose() */, (Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this);
	}

IL_0061:
	{
		return (bool)0;
	}
}
// System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable/WhereListIterator`1<System.Object>::Where(System.Func`2<TSource,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* WhereListIterator_1_Where_mD1D1F307DE1E555A5F7237BCA2C32947BCF6A14D_gshared (WhereListIterator_1_t1F40F08BAF8586F2C09294085BC605CC2FA432EB* __this, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate0, const RuntimeMethod* method) 
{
	{
		List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* L_0 = (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D*)__this->___source_3;
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_1 = (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*)__this->___predicate_4;
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_2 = ___predicate0;
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_3;
		L_3 = Enumerable_CombinePredicates_TisRuntimeObject_m613479C29B013E8FC2987E22F42A3BC6CC2C9768(L_1, L_2, il2cpp_rgctx_method(method->klass->rgctx_data, 14));
		WhereListIterator_1_t1F40F08BAF8586F2C09294085BC605CC2FA432EB* L_4 = (WhereListIterator_1_t1F40F08BAF8586F2C09294085BC605CC2FA432EB*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 3));
		NullCheck(L_4);
		WhereListIterator_1__ctor_mEF8F62B9078E538C1DC46BCB876C2AC5B29EA73B(L_4, L_0, L_3, il2cpp_rgctx_method(method->klass->rgctx_data, 5));
		return (RuntimeObject*)L_4;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void System.Linq.Enumerable/WhereListIterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::.ctor(System.Collections.Generic.List`1<TSource>,System.Func`2<TSource,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereListIterator_1__ctor_mC075454926AF320E4679335A1B81D3F56ACEFC0C_gshared (/*System.Linq.Enumerable/WhereListIterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>*/WhereListIterator_1_tD37742ECD2F53395BA8B668C2671C4C82E8E85F0* __this, List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* ___source0, Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* ___predicate1, const RuntimeMethod* method) 
{
	{
		InvokerActionInvoker0::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 0)), il2cpp_rgctx_method(method->klass->rgctx_data, 0), (Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0*)__this);
		List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* L_0 = ___source0;
		il2cpp_codegen_write_instance_field_data<List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),0), L_0);
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_1 = ___predicate1;
		il2cpp_codegen_write_instance_field_data<Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B*>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),1), L_1);
		return;
	}
}
// System.Linq.Enumerable/Iterator`1<TSource> System.Linq.Enumerable/WhereListIterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::Clone()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0* WhereListIterator_1_Clone_mAA3ED56493E5FF2F49FE37EB7CDF6C0A957698B5_gshared (/*System.Linq.Enumerable/WhereListIterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>*/WhereListIterator_1_tD37742ECD2F53395BA8B668C2671C4C82E8E85F0* __this, const RuntimeMethod* method) 
{
	{
		List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* L_0 = *(List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),0));
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_1 = *(Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),1));
		WhereListIterator_1_tD37742ECD2F53395BA8B668C2671C4C82E8E85F0* L_2 = (WhereListIterator_1_tD37742ECD2F53395BA8B668C2671C4C82E8E85F0*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 3));
		NullCheck(L_2);
		InvokerActionInvoker2< List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 5)), il2cpp_rgctx_method(method->klass->rgctx_data, 5), L_2, L_0, L_1);
		return (Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0*)L_2;
	}
}
// System.Boolean System.Linq.Enumerable/WhereListIterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool WhereListIterator_1_MoveNext_mB5E4EB089AD8CF7156B8972C7FB61739C466ED5E_gshared (/*System.Linq.Enumerable/WhereListIterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>*/WhereListIterator_1_tD37742ECD2F53395BA8B668C2671C4C82E8E85F0* __this, const RuntimeMethod* method) 
{
	// sizeof(TSource)
	const uint32_t SizeOf_TSource_t85B7C93A555823AE666813BFFC5FEC432E108956 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->klass->rgctx_data, 10));
	// sizeof(System.Collections.Generic.List`1/Enumerator<TSource>)
	const uint32_t SizeOf_Enumerator_t8E62FE91E95BFC5D28A3B09EFA69C2A33120205E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->klass->rgctx_data, 8));
	// TSource
	const Il2CppFullySharedGenericAny L_5 = alloca(SizeOf_TSource_t85B7C93A555823AE666813BFFC5FEC432E108956);
	const Il2CppFullySharedGenericAny L_9 = L_5;
	const Il2CppFullySharedGenericAny L_7 = alloca(SizeOf_TSource_t85B7C93A555823AE666813BFFC5FEC432E108956);
	// System.Collections.Generic.List`1/Enumerator<TSource>
	const Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF L_4 = alloca(SizeOf_Enumerator_t8E62FE91E95BFC5D28A3B09EFA69C2A33120205E);
	int32_t V_0 = 0;
	Il2CppFullySharedGenericAny V_1 = alloca(SizeOf_TSource_t85B7C93A555823AE666813BFFC5FEC432E108956);
	memset(V_1, 0, SizeOf_TSource_t85B7C93A555823AE666813BFFC5FEC432E108956);
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 6),1));
		V_0 = L_0;
		int32_t L_1 = V_0;
		if ((((int32_t)L_1) == ((int32_t)1)))
		{
			goto IL_0011;
		}
	}
	{
		int32_t L_2 = V_0;
		if ((((int32_t)L_2) == ((int32_t)2)))
		{
			goto IL_004e;
		}
	}
	{
		goto IL_0061;
	}

IL_0011:
	{
		List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* L_3 = *(List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),0));
		NullCheck(L_3);
		InvokerActionInvoker1< Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 7)), il2cpp_rgctx_method(method->klass->rgctx_data, 7), L_3, (Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF*)L_4);
		il2cpp_codegen_write_instance_field_data(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),2), L_4, SizeOf_Enumerator_t8E62FE91E95BFC5D28A3B09EFA69C2A33120205E);
		il2cpp_codegen_write_instance_field_data<int32_t>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 6),1), 2);
		goto IL_004e;
	}

IL_002b:
	{
		InvokerActionInvoker1< Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 9)), il2cpp_rgctx_method(method->klass->rgctx_data, 9), (((Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF*)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),2)))), (Il2CppFullySharedGenericAny*)L_5);
		il2cpp_codegen_memcpy(V_1, L_5, SizeOf_TSource_t85B7C93A555823AE666813BFFC5FEC432E108956);
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_6 = *(Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),1));
		il2cpp_codegen_memcpy(L_7, V_1, SizeOf_TSource_t85B7C93A555823AE666813BFFC5FEC432E108956);
		NullCheck(L_6);
		bool L_8;
		L_8 = InvokerFuncInvoker1< bool, Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 11)), il2cpp_rgctx_method(method->klass->rgctx_data, 11), L_6, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->klass->rgctx_data, 10)) ? L_7: *(void**)L_7));
		if (!L_8)
		{
			goto IL_004e;
		}
	}
	{
		il2cpp_codegen_memcpy(L_9, V_1, SizeOf_TSource_t85B7C93A555823AE666813BFFC5FEC432E108956);
		il2cpp_codegen_write_instance_field_data(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 6),2), L_9, SizeOf_TSource_t85B7C93A555823AE666813BFFC5FEC432E108956);
		return (bool)1;
	}

IL_004e:
	{
		bool L_10;
		L_10 = InvokerFuncInvoker0< bool >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 12)), il2cpp_rgctx_method(method->klass->rgctx_data, 12), (((Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF*)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),2)))));
		if (L_10)
		{
			goto IL_002b;
		}
	}
	{
		NullCheck((Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0*)__this);
		VirtualActionInvoker0Invoker::Invoke(12 /* System.Void System.Linq.Enumerable/Iterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::Dispose() */, (Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0*)__this);
	}

IL_0061:
	{
		return (bool)0;
	}
}
// System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable/WhereListIterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::Where(System.Func`2<TSource,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* WhereListIterator_1_Where_mC767815DE2249E70B38D6D172A0C61B028D7A44B_gshared (/*System.Linq.Enumerable/WhereListIterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>*/WhereListIterator_1_tD37742ECD2F53395BA8B668C2671C4C82E8E85F0* __this, Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* ___predicate0, const RuntimeMethod* method) 
{
	{
		List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* L_0 = *(List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),0));
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_1 = *(Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),1));
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_2 = ___predicate0;
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_3;
		L_3 = InvokerFuncInvoker2< Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B*, Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B*, Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 14)), il2cpp_rgctx_method(method->klass->rgctx_data, 14), NULL, L_1, L_2);
		WhereListIterator_1_tD37742ECD2F53395BA8B668C2671C4C82E8E85F0* L_4 = (WhereListIterator_1_tD37742ECD2F53395BA8B668C2671C4C82E8E85F0*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 3));
		NullCheck(L_4);
		InvokerActionInvoker2< List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 5)), il2cpp_rgctx_method(method->klass->rgctx_data, 5), L_4, L_0, L_3);
		return (RuntimeObject*)L_4;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void System.Linq.Enumerable/WhereSelectArrayIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::.ctor(TSource[],System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectArrayIterator_2__ctor_m5DCBBB86A9209D1F91CCB6D656EDCD6A2AF93A31_gshared (WhereSelectArrayIterator_2_t30000EF5692CE8B73879E35CB9EA48B4DCF6EAB5* __this, ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46* ___source0, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate1, Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B* ___selector2, const RuntimeMethod* method) 
{
	{
		Iterator_1__ctor_m27CEA132FC2E7D7BA6A8529CE0647F2709FC29A3((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 0));
		ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46* L_0 = ___source0;
		__this->___source_3 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___source_3), (void*)L_0);
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_1 = ___predicate1;
		__this->___predicate_4 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___predicate_4), (void*)L_1);
		Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B* L_2 = ___selector2;
		__this->___selector_5 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___selector_5), (void*)L_2);
		return;
	}
}
// System.Linq.Enumerable/Iterator`1<TResult> System.Linq.Enumerable/WhereSelectArrayIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::Clone()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40* WhereSelectArrayIterator_2_Clone_mB65EEBE862DEE973D921A3B11F124150EE01D0B9_gshared (WhereSelectArrayIterator_2_t30000EF5692CE8B73879E35CB9EA48B4DCF6EAB5* __this, const RuntimeMethod* method) 
{
	{
		ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46* L_0 = (ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46*)__this->___source_3;
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_1 = (Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*)__this->___predicate_4;
		Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B* L_2 = (Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B*)__this->___selector_5;
		WhereSelectArrayIterator_2_t30000EF5692CE8B73879E35CB9EA48B4DCF6EAB5* L_3 = (WhereSelectArrayIterator_2_t30000EF5692CE8B73879E35CB9EA48B4DCF6EAB5*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 3));
		NullCheck(L_3);
		WhereSelectArrayIterator_2__ctor_m5DCBBB86A9209D1F91CCB6D656EDCD6A2AF93A31(L_3, L_0, L_1, L_2, il2cpp_rgctx_method(method->klass->rgctx_data, 6));
		return (Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)L_3;
	}
}
// System.Boolean System.Linq.Enumerable/WhereSelectArrayIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool WhereSelectArrayIterator_2_MoveNext_mBA9CEDB92A849D9613D6393E06914B8815CD7C3B_gshared (WhereSelectArrayIterator_2_t30000EF5692CE8B73879E35CB9EA48B4DCF6EAB5* __this, const RuntimeMethod* method) 
{
	ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = (int32_t)((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this)->___state_1;
		if ((!(((uint32_t)L_0) == ((uint32_t)1))))
		{
			goto IL_006b;
		}
	}
	{
		goto IL_0055;
	}

IL_000b:
	{
		ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46* L_1 = (ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46*)__this->___source_3;
		int32_t L_2 = (int32_t)__this->___index_6;
		NullCheck(L_1);
		int32_t L_3 = L_2;
		ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 L_4 = (L_1)->GetAt(static_cast<il2cpp_array_size_t>(L_3));
		V_0 = L_4;
		int32_t L_5 = (int32_t)__this->___index_6;
		__this->___index_6 = ((int32_t)il2cpp_codegen_add(L_5, 1));
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_6 = (Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*)__this->___predicate_4;
		if (!L_6)
		{
			goto IL_0041;
		}
	}
	{
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_7 = (Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*)__this->___predicate_4;
		ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 L_8 = V_0;
		NullCheck(L_7);
		bool L_9;
		L_9 = Func_2_Invoke_m12B4F5C7D4C30A85BD18305B0CDE8DF42312F402_inline(L_7, L_8, il2cpp_rgctx_method(method->klass->rgctx_data, 9));
		if (!L_9)
		{
			goto IL_0055;
		}
	}

IL_0041:
	{
		Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B* L_10 = (Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B*)__this->___selector_5;
		ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 L_11 = V_0;
		NullCheck(L_10);
		ValueTuple_2_t761B36235D5F97F497A1CBCD077DF3858814FADE L_12;
		L_12 = Func_2_Invoke_m3794F6EAED760CA17FC7A24439DC423AC6BF8FB9_inline(L_10, L_11, il2cpp_rgctx_method(method->klass->rgctx_data, 10));
		((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this)->___current_2 = L_12;
		Il2CppCodeGenWriteBarrier((void**)&(((&((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this)->___current_2))->___Item1_0), (void*)NULL);
		return (bool)1;
	}

IL_0055:
	{
		int32_t L_13 = (int32_t)__this->___index_6;
		ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46* L_14 = (ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46*)__this->___source_3;
		NullCheck(L_14);
		if ((((int32_t)L_13) < ((int32_t)((int32_t)(((RuntimeArray*)L_14)->max_length)))))
		{
			goto IL_000b;
		}
	}
	{
		NullCheck((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this);
		VirtualActionInvoker0::Invoke(12 /* System.Void System.Linq.Enumerable/Iterator`1<System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::Dispose() */, (Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this);
	}

IL_006b:
	{
		return (bool)0;
	}
}
// System.Collections.Generic.IEnumerable`1<TResult> System.Linq.Enumerable/WhereSelectArrayIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::Where(System.Func`2<TResult,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* WhereSelectArrayIterator_2_Where_m79E18C2339AD3BC48E306FB8A17820E28930A19A_gshared (WhereSelectArrayIterator_2_t30000EF5692CE8B73879E35CB9EA48B4DCF6EAB5* __this, Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B* ___predicate0, const RuntimeMethod* method) 
{
	{
		Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B* L_0 = ___predicate0;
		WhereEnumerableIterator_1_tB576B4EE76F70B81DDF11FA35260EC589825E7C5* L_1 = (WhereEnumerableIterator_1_tB576B4EE76F70B81DDF11FA35260EC589825E7C5*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 14));
		NullCheck(L_1);
		WhereEnumerableIterator_1__ctor_mC87BD639674AE32961C0AC5A5878F4144E7DA780(L_1, (RuntimeObject*)__this, L_0, il2cpp_rgctx_method(method->klass->rgctx_data, 15));
		return (RuntimeObject*)L_1;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void System.Linq.Enumerable/WhereSelectArrayIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Object>::.ctor(TSource[],System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectArrayIterator_2__ctor_m7932AB67118B332F6FAEB44AA075721F4C0BC26B_gshared (WhereSelectArrayIterator_2_tBABE519E8262C3B19F12FA57F0EBBEF62306E2EE* __this, ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46* ___source0, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate1, Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287* ___selector2, const RuntimeMethod* method) 
{
	{
		Iterator_1__ctor_m3E47867714E05673E54C6B73D9242F5FFADA1F63((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 0));
		ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46* L_0 = ___source0;
		__this->___source_3 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___source_3), (void*)L_0);
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_1 = ___predicate1;
		__this->___predicate_4 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___predicate_4), (void*)L_1);
		Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287* L_2 = ___selector2;
		__this->___selector_5 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___selector_5), (void*)L_2);
		return;
	}
}
// System.Linq.Enumerable/Iterator`1<TResult> System.Linq.Enumerable/WhereSelectArrayIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Object>::Clone()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA* WhereSelectArrayIterator_2_Clone_mAF3E0D8DA8B4F392932C24EC7C0E76BA601681E7_gshared (WhereSelectArrayIterator_2_tBABE519E8262C3B19F12FA57F0EBBEF62306E2EE* __this, const RuntimeMethod* method) 
{
	{
		ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46* L_0 = (ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46*)__this->___source_3;
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_1 = (Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*)__this->___predicate_4;
		Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287* L_2 = (Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287*)__this->___selector_5;
		WhereSelectArrayIterator_2_tBABE519E8262C3B19F12FA57F0EBBEF62306E2EE* L_3 = (WhereSelectArrayIterator_2_tBABE519E8262C3B19F12FA57F0EBBEF62306E2EE*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 3));
		NullCheck(L_3);
		WhereSelectArrayIterator_2__ctor_m7932AB67118B332F6FAEB44AA075721F4C0BC26B(L_3, L_0, L_1, L_2, il2cpp_rgctx_method(method->klass->rgctx_data, 6));
		return (Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)L_3;
	}
}
// System.Boolean System.Linq.Enumerable/WhereSelectArrayIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Object>::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool WhereSelectArrayIterator_2_MoveNext_m852FA448E75E8EDD5B9E96CDA611A407A5975799_gshared (WhereSelectArrayIterator_2_tBABE519E8262C3B19F12FA57F0EBBEF62306E2EE* __this, const RuntimeMethod* method) 
{
	ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = (int32_t)((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this)->___state_1;
		if ((!(((uint32_t)L_0) == ((uint32_t)1))))
		{
			goto IL_006b;
		}
	}
	{
		goto IL_0055;
	}

IL_000b:
	{
		ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46* L_1 = (ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46*)__this->___source_3;
		int32_t L_2 = (int32_t)__this->___index_6;
		NullCheck(L_1);
		int32_t L_3 = L_2;
		ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 L_4 = (L_1)->GetAt(static_cast<il2cpp_array_size_t>(L_3));
		V_0 = L_4;
		int32_t L_5 = (int32_t)__this->___index_6;
		__this->___index_6 = ((int32_t)il2cpp_codegen_add(L_5, 1));
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_6 = (Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*)__this->___predicate_4;
		if (!L_6)
		{
			goto IL_0041;
		}
	}
	{
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_7 = (Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*)__this->___predicate_4;
		ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 L_8 = V_0;
		NullCheck(L_7);
		bool L_9;
		L_9 = Func_2_Invoke_m12B4F5C7D4C30A85BD18305B0CDE8DF42312F402_inline(L_7, L_8, il2cpp_rgctx_method(method->klass->rgctx_data, 9));
		if (!L_9)
		{
			goto IL_0055;
		}
	}

IL_0041:
	{
		Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287* L_10 = (Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287*)__this->___selector_5;
		ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 L_11 = V_0;
		NullCheck(L_10);
		RuntimeObject* L_12;
		L_12 = Func_2_Invoke_m9EB50B06293708FB57A5A30A460483868FFF99D0_inline(L_10, L_11, il2cpp_rgctx_method(method->klass->rgctx_data, 10));
		((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this)->___current_2 = L_12;
		Il2CppCodeGenWriteBarrier((void**)(&((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this)->___current_2), (void*)L_12);
		return (bool)1;
	}

IL_0055:
	{
		int32_t L_13 = (int32_t)__this->___index_6;
		ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46* L_14 = (ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46*)__this->___source_3;
		NullCheck(L_14);
		if ((((int32_t)L_13) < ((int32_t)((int32_t)(((RuntimeArray*)L_14)->max_length)))))
		{
			goto IL_000b;
		}
	}
	{
		NullCheck((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this);
		VirtualActionInvoker0::Invoke(12 /* System.Void System.Linq.Enumerable/Iterator`1<System.Object>::Dispose() */, (Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this);
	}

IL_006b:
	{
		return (bool)0;
	}
}
// System.Collections.Generic.IEnumerable`1<TResult> System.Linq.Enumerable/WhereSelectArrayIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Object>::Where(System.Func`2<TResult,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* WhereSelectArrayIterator_2_Where_mC512C4A9FBDE624D78F250E26B9F5BE8C5F55727_gshared (WhereSelectArrayIterator_2_tBABE519E8262C3B19F12FA57F0EBBEF62306E2EE* __this, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate0, const RuntimeMethod* method) 
{
	{
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_0 = ___predicate0;
		WhereEnumerableIterator_1_t1E787D13759F5A31C94B3FAED181402B25C278F4* L_1 = (WhereEnumerableIterator_1_t1E787D13759F5A31C94B3FAED181402B25C278F4*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 14));
		NullCheck(L_1);
		WhereEnumerableIterator_1__ctor_m8C0DA4CDA5431C03561F67C4393BB18CDD891F01(L_1, (RuntimeObject*)__this, L_0, il2cpp_rgctx_method(method->klass->rgctx_data, 15));
		return (RuntimeObject*)L_1;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void System.Linq.Enumerable/WhereSelectArrayIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Single>::.ctor(TSource[],System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectArrayIterator_2__ctor_mA507C5E2A96C465B089EC01D8C9D02D9D5D3D0E4_gshared (WhereSelectArrayIterator_2_tFF39CC0F264CA7C80FEA868C00FD12A1D45DC58F* __this, ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46* ___source0, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate1, Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72* ___selector2, const RuntimeMethod* method) 
{
	{
		Iterator_1__ctor_m7FEBAE985CDE5DD1CB255EB4A3D65F05AA8D2AEF((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 0));
		ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46* L_0 = ___source0;
		__this->___source_3 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___source_3), (void*)L_0);
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_1 = ___predicate1;
		__this->___predicate_4 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___predicate_4), (void*)L_1);
		Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72* L_2 = ___selector2;
		__this->___selector_5 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___selector_5), (void*)L_2);
		return;
	}
}
// System.Linq.Enumerable/Iterator`1<TResult> System.Linq.Enumerable/WhereSelectArrayIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Single>::Clone()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E* WhereSelectArrayIterator_2_Clone_mA8BB8D929F66B00EEBF445850E7134E7E7B946B9_gshared (WhereSelectArrayIterator_2_tFF39CC0F264CA7C80FEA868C00FD12A1D45DC58F* __this, const RuntimeMethod* method) 
{
	{
		ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46* L_0 = (ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46*)__this->___source_3;
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_1 = (Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*)__this->___predicate_4;
		Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72* L_2 = (Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72*)__this->___selector_5;
		WhereSelectArrayIterator_2_tFF39CC0F264CA7C80FEA868C00FD12A1D45DC58F* L_3 = (WhereSelectArrayIterator_2_tFF39CC0F264CA7C80FEA868C00FD12A1D45DC58F*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 3));
		NullCheck(L_3);
		WhereSelectArrayIterator_2__ctor_mA507C5E2A96C465B089EC01D8C9D02D9D5D3D0E4(L_3, L_0, L_1, L_2, il2cpp_rgctx_method(method->klass->rgctx_data, 6));
		return (Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)L_3;
	}
}
// System.Boolean System.Linq.Enumerable/WhereSelectArrayIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Single>::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool WhereSelectArrayIterator_2_MoveNext_mF64FE805287ADAA90CD9EA33D02A8574F7D69638_gshared (WhereSelectArrayIterator_2_tFF39CC0F264CA7C80FEA868C00FD12A1D45DC58F* __this, const RuntimeMethod* method) 
{
	ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = (int32_t)((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this)->___state_1;
		if ((!(((uint32_t)L_0) == ((uint32_t)1))))
		{
			goto IL_006b;
		}
	}
	{
		goto IL_0055;
	}

IL_000b:
	{
		ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46* L_1 = (ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46*)__this->___source_3;
		int32_t L_2 = (int32_t)__this->___index_6;
		NullCheck(L_1);
		int32_t L_3 = L_2;
		ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 L_4 = (L_1)->GetAt(static_cast<il2cpp_array_size_t>(L_3));
		V_0 = L_4;
		int32_t L_5 = (int32_t)__this->___index_6;
		__this->___index_6 = ((int32_t)il2cpp_codegen_add(L_5, 1));
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_6 = (Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*)__this->___predicate_4;
		if (!L_6)
		{
			goto IL_0041;
		}
	}
	{
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_7 = (Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*)__this->___predicate_4;
		ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 L_8 = V_0;
		NullCheck(L_7);
		bool L_9;
		L_9 = Func_2_Invoke_m12B4F5C7D4C30A85BD18305B0CDE8DF42312F402_inline(L_7, L_8, il2cpp_rgctx_method(method->klass->rgctx_data, 9));
		if (!L_9)
		{
			goto IL_0055;
		}
	}

IL_0041:
	{
		Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72* L_10 = (Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72*)__this->___selector_5;
		ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 L_11 = V_0;
		NullCheck(L_10);
		float L_12;
		L_12 = Func_2_Invoke_mC888CBA13A68639AC714989A4F3DC91C432BB307_inline(L_10, L_11, il2cpp_rgctx_method(method->klass->rgctx_data, 10));
		((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this)->___current_2 = L_12;
		return (bool)1;
	}

IL_0055:
	{
		int32_t L_13 = (int32_t)__this->___index_6;
		ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46* L_14 = (ValueTuple_3U5BU5D_t3EA47529B3B5CCC3AA19D366771487D0C4438C46*)__this->___source_3;
		NullCheck(L_14);
		if ((((int32_t)L_13) < ((int32_t)((int32_t)(((RuntimeArray*)L_14)->max_length)))))
		{
			goto IL_000b;
		}
	}
	{
		NullCheck((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this);
		VirtualActionInvoker0::Invoke(12 /* System.Void System.Linq.Enumerable/Iterator`1<System.Single>::Dispose() */, (Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this);
	}

IL_006b:
	{
		return (bool)0;
	}
}
// System.Collections.Generic.IEnumerable`1<TResult> System.Linq.Enumerable/WhereSelectArrayIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Single>::Where(System.Func`2<TResult,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* WhereSelectArrayIterator_2_Where_mE4DE18DAA26515442E26A684F10EC8750E81EFAC_gshared (WhereSelectArrayIterator_2_tFF39CC0F264CA7C80FEA868C00FD12A1D45DC58F* __this, Func_2_t49E998685259ADE759F9329BF66F20DE8667006E* ___predicate0, const RuntimeMethod* method) 
{
	{
		Func_2_t49E998685259ADE759F9329BF66F20DE8667006E* L_0 = ___predicate0;
		WhereEnumerableIterator_1_t7BB2D1D9F8A6E52243A87F45DFEEA4209D685F7C* L_1 = (WhereEnumerableIterator_1_t7BB2D1D9F8A6E52243A87F45DFEEA4209D685F7C*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 14));
		NullCheck(L_1);
		WhereEnumerableIterator_1__ctor_mCB49A03958EF827EF6CE47402259941EAB31D984(L_1, (RuntimeObject*)__this, L_0, il2cpp_rgctx_method(method->klass->rgctx_data, 15));
		return (RuntimeObject*)L_1;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void System.Linq.Enumerable/WhereSelectArrayIterator`2<System.Object,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::.ctor(TSource[],System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectArrayIterator_2__ctor_m542DF3BD7D0591BD1A6CF2AC84E55CB5431BBDD7_gshared (WhereSelectArrayIterator_2_tB6BDCFF0C2C4DEEFA9D69A00383291FAA9B56E24* __this, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___source0, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate1, Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9* ___selector2, const RuntimeMethod* method) 
{
	{
		Iterator_1__ctor_m27CEA132FC2E7D7BA6A8529CE0647F2709FC29A3((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 0));
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_0 = ___source0;
		__this->___source_3 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___source_3), (void*)L_0);
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_1 = ___predicate1;
		__this->___predicate_4 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___predicate_4), (void*)L_1);
		Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9* L_2 = ___selector2;
		__this->___selector_5 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___selector_5), (void*)L_2);
		return;
	}
}
// System.Linq.Enumerable/Iterator`1<TResult> System.Linq.Enumerable/WhereSelectArrayIterator`2<System.Object,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::Clone()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40* WhereSelectArrayIterator_2_Clone_m1C91E34928B744C19D2F9962DB11BFD03372ADFD_gshared (WhereSelectArrayIterator_2_tB6BDCFF0C2C4DEEFA9D69A00383291FAA9B56E24* __this, const RuntimeMethod* method) 
{
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_0 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)__this->___source_3;
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_1 = (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*)__this->___predicate_4;
		Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9* L_2 = (Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9*)__this->___selector_5;
		WhereSelectArrayIterator_2_tB6BDCFF0C2C4DEEFA9D69A00383291FAA9B56E24* L_3 = (WhereSelectArrayIterator_2_tB6BDCFF0C2C4DEEFA9D69A00383291FAA9B56E24*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 3));
		NullCheck(L_3);
		WhereSelectArrayIterator_2__ctor_m542DF3BD7D0591BD1A6CF2AC84E55CB5431BBDD7(L_3, L_0, L_1, L_2, il2cpp_rgctx_method(method->klass->rgctx_data, 6));
		return (Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)L_3;
	}
}
// System.Boolean System.Linq.Enumerable/WhereSelectArrayIterator`2<System.Object,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool WhereSelectArrayIterator_2_MoveNext_m49971912657E76CFA80601F5FED97AB5D66264A0_gshared (WhereSelectArrayIterator_2_tB6BDCFF0C2C4DEEFA9D69A00383291FAA9B56E24* __this, const RuntimeMethod* method) 
{
	RuntimeObject* V_0 = NULL;
	{
		int32_t L_0 = (int32_t)((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this)->___state_1;
		if ((!(((uint32_t)L_0) == ((uint32_t)1))))
		{
			goto IL_006b;
		}
	}
	{
		goto IL_0055;
	}

IL_000b:
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)__this->___source_3;
		int32_t L_2 = (int32_t)__this->___index_6;
		NullCheck(L_1);
		int32_t L_3 = L_2;
		RuntimeObject* L_4 = (L_1)->GetAt(static_cast<il2cpp_array_size_t>(L_3));
		V_0 = L_4;
		int32_t L_5 = (int32_t)__this->___index_6;
		__this->___index_6 = ((int32_t)il2cpp_codegen_add(L_5, 1));
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_6 = (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*)__this->___predicate_4;
		if (!L_6)
		{
			goto IL_0041;
		}
	}
	{
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_7 = (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*)__this->___predicate_4;
		RuntimeObject* L_8 = V_0;
		NullCheck(L_7);
		bool L_9;
		L_9 = Func_2_Invoke_m2014423FB900F135C8FF994125604FF9E6AAE829_inline(L_7, L_8, il2cpp_rgctx_method(method->klass->rgctx_data, 9));
		if (!L_9)
		{
			goto IL_0055;
		}
	}

IL_0041:
	{
		Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9* L_10 = (Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9*)__this->___selector_5;
		RuntimeObject* L_11 = V_0;
		NullCheck(L_10);
		ValueTuple_2_t761B36235D5F97F497A1CBCD077DF3858814FADE L_12;
		L_12 = Func_2_Invoke_m30EE337C21F1CA674046A64A8A2DA68A02701634_inline(L_10, L_11, il2cpp_rgctx_method(method->klass->rgctx_data, 10));
		((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this)->___current_2 = L_12;
		Il2CppCodeGenWriteBarrier((void**)&(((&((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this)->___current_2))->___Item1_0), (void*)NULL);
		return (bool)1;
	}

IL_0055:
	{
		int32_t L_13 = (int32_t)__this->___index_6;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_14 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)__this->___source_3;
		NullCheck(L_14);
		if ((((int32_t)L_13) < ((int32_t)((int32_t)(((RuntimeArray*)L_14)->max_length)))))
		{
			goto IL_000b;
		}
	}
	{
		NullCheck((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this);
		VirtualActionInvoker0::Invoke(12 /* System.Void System.Linq.Enumerable/Iterator`1<System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::Dispose() */, (Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this);
	}

IL_006b:
	{
		return (bool)0;
	}
}
// System.Collections.Generic.IEnumerable`1<TResult> System.Linq.Enumerable/WhereSelectArrayIterator`2<System.Object,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::Where(System.Func`2<TResult,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* WhereSelectArrayIterator_2_Where_mE321CA39368C5649DC33622923B399B95F29724D_gshared (WhereSelectArrayIterator_2_tB6BDCFF0C2C4DEEFA9D69A00383291FAA9B56E24* __this, Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B* ___predicate0, const RuntimeMethod* method) 
{
	{
		Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B* L_0 = ___predicate0;
		WhereEnumerableIterator_1_tB576B4EE76F70B81DDF11FA35260EC589825E7C5* L_1 = (WhereEnumerableIterator_1_tB576B4EE76F70B81DDF11FA35260EC589825E7C5*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 14));
		NullCheck(L_1);
		WhereEnumerableIterator_1__ctor_mC87BD639674AE32961C0AC5A5878F4144E7DA780(L_1, (RuntimeObject*)__this, L_0, il2cpp_rgctx_method(method->klass->rgctx_data, 15));
		return (RuntimeObject*)L_1;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void System.Linq.Enumerable/WhereSelectArrayIterator`2<System.Object,System.Single>::.ctor(TSource[],System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectArrayIterator_2__ctor_m98F1E0E34745862A883CE6A1CCE4DE42E16CF8A9_gshared (WhereSelectArrayIterator_2_tD87299FD8280F38D5E50B5174574F2F20188E0D4* __this, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___source0, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate1, Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12* ___selector2, const RuntimeMethod* method) 
{
	{
		Iterator_1__ctor_m7FEBAE985CDE5DD1CB255EB4A3D65F05AA8D2AEF((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 0));
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_0 = ___source0;
		__this->___source_3 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___source_3), (void*)L_0);
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_1 = ___predicate1;
		__this->___predicate_4 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___predicate_4), (void*)L_1);
		Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12* L_2 = ___selector2;
		__this->___selector_5 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___selector_5), (void*)L_2);
		return;
	}
}
// System.Linq.Enumerable/Iterator`1<TResult> System.Linq.Enumerable/WhereSelectArrayIterator`2<System.Object,System.Single>::Clone()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E* WhereSelectArrayIterator_2_Clone_m123B4142A47E468F5409486044E297CC3AA11A50_gshared (WhereSelectArrayIterator_2_tD87299FD8280F38D5E50B5174574F2F20188E0D4* __this, const RuntimeMethod* method) 
{
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_0 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)__this->___source_3;
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_1 = (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*)__this->___predicate_4;
		Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12* L_2 = (Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12*)__this->___selector_5;
		WhereSelectArrayIterator_2_tD87299FD8280F38D5E50B5174574F2F20188E0D4* L_3 = (WhereSelectArrayIterator_2_tD87299FD8280F38D5E50B5174574F2F20188E0D4*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 3));
		NullCheck(L_3);
		WhereSelectArrayIterator_2__ctor_m98F1E0E34745862A883CE6A1CCE4DE42E16CF8A9(L_3, L_0, L_1, L_2, il2cpp_rgctx_method(method->klass->rgctx_data, 6));
		return (Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)L_3;
	}
}
// System.Boolean System.Linq.Enumerable/WhereSelectArrayIterator`2<System.Object,System.Single>::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool WhereSelectArrayIterator_2_MoveNext_m2DA41E8CCD9EEFE703E363CAFACFFEF7E05C3BAF_gshared (WhereSelectArrayIterator_2_tD87299FD8280F38D5E50B5174574F2F20188E0D4* __this, const RuntimeMethod* method) 
{
	RuntimeObject* V_0 = NULL;
	{
		int32_t L_0 = (int32_t)((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this)->___state_1;
		if ((!(((uint32_t)L_0) == ((uint32_t)1))))
		{
			goto IL_006b;
		}
	}
	{
		goto IL_0055;
	}

IL_000b:
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)__this->___source_3;
		int32_t L_2 = (int32_t)__this->___index_6;
		NullCheck(L_1);
		int32_t L_3 = L_2;
		RuntimeObject* L_4 = (L_1)->GetAt(static_cast<il2cpp_array_size_t>(L_3));
		V_0 = L_4;
		int32_t L_5 = (int32_t)__this->___index_6;
		__this->___index_6 = ((int32_t)il2cpp_codegen_add(L_5, 1));
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_6 = (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*)__this->___predicate_4;
		if (!L_6)
		{
			goto IL_0041;
		}
	}
	{
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_7 = (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*)__this->___predicate_4;
		RuntimeObject* L_8 = V_0;
		NullCheck(L_7);
		bool L_9;
		L_9 = Func_2_Invoke_m2014423FB900F135C8FF994125604FF9E6AAE829_inline(L_7, L_8, il2cpp_rgctx_method(method->klass->rgctx_data, 9));
		if (!L_9)
		{
			goto IL_0055;
		}
	}

IL_0041:
	{
		Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12* L_10 = (Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12*)__this->___selector_5;
		RuntimeObject* L_11 = V_0;
		NullCheck(L_10);
		float L_12;
		L_12 = Func_2_Invoke_mBE16A5FDA5E80CCBA51D69334EF21C0F03D353AF_inline(L_10, L_11, il2cpp_rgctx_method(method->klass->rgctx_data, 10));
		((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this)->___current_2 = L_12;
		return (bool)1;
	}

IL_0055:
	{
		int32_t L_13 = (int32_t)__this->___index_6;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_14 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)__this->___source_3;
		NullCheck(L_14);
		if ((((int32_t)L_13) < ((int32_t)((int32_t)(((RuntimeArray*)L_14)->max_length)))))
		{
			goto IL_000b;
		}
	}
	{
		NullCheck((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this);
		VirtualActionInvoker0::Invoke(12 /* System.Void System.Linq.Enumerable/Iterator`1<System.Single>::Dispose() */, (Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this);
	}

IL_006b:
	{
		return (bool)0;
	}
}
// System.Collections.Generic.IEnumerable`1<TResult> System.Linq.Enumerable/WhereSelectArrayIterator`2<System.Object,System.Single>::Where(System.Func`2<TResult,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* WhereSelectArrayIterator_2_Where_mDB045FAD713AF0D4B4D974E03BD7025892E75AFA_gshared (WhereSelectArrayIterator_2_tD87299FD8280F38D5E50B5174574F2F20188E0D4* __this, Func_2_t49E998685259ADE759F9329BF66F20DE8667006E* ___predicate0, const RuntimeMethod* method) 
{
	{
		Func_2_t49E998685259ADE759F9329BF66F20DE8667006E* L_0 = ___predicate0;
		WhereEnumerableIterator_1_t7BB2D1D9F8A6E52243A87F45DFEEA4209D685F7C* L_1 = (WhereEnumerableIterator_1_t7BB2D1D9F8A6E52243A87F45DFEEA4209D685F7C*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 14));
		NullCheck(L_1);
		WhereEnumerableIterator_1__ctor_mCB49A03958EF827EF6CE47402259941EAB31D984(L_1, (RuntimeObject*)__this, L_0, il2cpp_rgctx_method(method->klass->rgctx_data, 15));
		return (RuntimeObject*)L_1;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void System.Linq.Enumerable/WhereSelectArrayIterator`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::.ctor(TSource[],System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectArrayIterator_2__ctor_mB15DB27A8DC3B4E00BCA6E8F63F00F7E374F76A4_gshared (/*System.Linq.Enumerable/WhereSelectArrayIterator`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>*/WhereSelectArrayIterator_2_tBE026CE497BB8F36E31685722BBD7CB567570174* __this, __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* ___source0, Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* ___predicate1, Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0* ___selector2, const RuntimeMethod* method) 
{
	{
		InvokerActionInvoker0::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 0)), il2cpp_rgctx_method(method->klass->rgctx_data, 0), (Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0*)__this);
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_0 = ___source0;
		il2cpp_codegen_write_instance_field_data<__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC*>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),0), L_0);
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_1 = ___predicate1;
		il2cpp_codegen_write_instance_field_data<Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B*>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),1), L_1);
		Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0* L_2 = ___selector2;
		il2cpp_codegen_write_instance_field_data<Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0*>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),2), L_2);
		return;
	}
}
// System.Linq.Enumerable/Iterator`1<TResult> System.Linq.Enumerable/WhereSelectArrayIterator`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::Clone()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0* WhereSelectArrayIterator_2_Clone_mFBF81AE0E2B6F7A7A79FC98398E7A6AC0FD330E9_gshared (/*System.Linq.Enumerable/WhereSelectArrayIterator`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>*/WhereSelectArrayIterator_2_tBE026CE497BB8F36E31685722BBD7CB567570174* __this, const RuntimeMethod* method) 
{
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_0 = *(__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),0));
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_1 = *(Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),1));
		Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0* L_2 = *(Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),2));
		WhereSelectArrayIterator_2_tBE026CE497BB8F36E31685722BBD7CB567570174* L_3 = (WhereSelectArrayIterator_2_tBE026CE497BB8F36E31685722BBD7CB567570174*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 3));
		NullCheck(L_3);
		InvokerActionInvoker3< __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC*, Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B*, Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 6)), il2cpp_rgctx_method(method->klass->rgctx_data, 6), L_3, L_0, L_1, L_2);
		return (Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0*)L_3;
	}
}
// System.Boolean System.Linq.Enumerable/WhereSelectArrayIterator`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool WhereSelectArrayIterator_2_MoveNext_mEF7E8E7B117D6D1147C53CAE838836974171392C_gshared (/*System.Linq.Enumerable/WhereSelectArrayIterator`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>*/WhereSelectArrayIterator_2_tBE026CE497BB8F36E31685722BBD7CB567570174* __this, const RuntimeMethod* method) 
{
	// sizeof(TSource)
	const uint32_t SizeOf_TSource_t21BF09076F270DC063711DE3ABB52B001A331F78 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->klass->rgctx_data, 8));
	// sizeof(TResult)
	const uint32_t SizeOf_TResult_t278B55150BC17BB45D33B605F011F4D96EFE5425 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->klass->rgctx_data, 11));
	// TSource
	const Il2CppFullySharedGenericAny L_4 = alloca(SizeOf_TSource_t21BF09076F270DC063711DE3ABB52B001A331F78);
	const Il2CppFullySharedGenericAny L_8 = L_4;
	const Il2CppFullySharedGenericAny L_11 = L_4;
	// TResult
	const Il2CppFullySharedGenericAny L_12 = alloca(SizeOf_TResult_t278B55150BC17BB45D33B605F011F4D96EFE5425);
	Il2CppFullySharedGenericAny V_0 = alloca(SizeOf_TSource_t21BF09076F270DC063711DE3ABB52B001A331F78);
	memset(V_0, 0, SizeOf_TSource_t21BF09076F270DC063711DE3ABB52B001A331F78);
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 7),1));
		if ((!(((uint32_t)L_0) == ((uint32_t)1))))
		{
			goto IL_006b;
		}
	}
	{
		goto IL_0055;
	}

IL_000b:
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_1 = *(__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),0));
		int32_t L_2 = *(int32_t*)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),3));
		NullCheck(L_1);
		int32_t L_3 = L_2;
		il2cpp_codegen_memcpy(L_4, (L_1)->GetAddressAt(static_cast<il2cpp_array_size_t>(L_3)), SizeOf_TSource_t21BF09076F270DC063711DE3ABB52B001A331F78);
		il2cpp_codegen_memcpy(V_0, L_4, SizeOf_TSource_t21BF09076F270DC063711DE3ABB52B001A331F78);
		int32_t L_5 = *(int32_t*)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),3));
		il2cpp_codegen_write_instance_field_data<int32_t>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),3), ((int32_t)il2cpp_codegen_add(L_5, 1)));
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_6 = *(Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),1));
		if (!L_6)
		{
			goto IL_0041;
		}
	}
	{
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_7 = *(Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),1));
		il2cpp_codegen_memcpy(L_8, V_0, SizeOf_TSource_t21BF09076F270DC063711DE3ABB52B001A331F78);
		NullCheck(L_7);
		bool L_9;
		L_9 = InvokerFuncInvoker1< bool, Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 9)), il2cpp_rgctx_method(method->klass->rgctx_data, 9), L_7, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->klass->rgctx_data, 8)) ? L_8: *(void**)L_8));
		if (!L_9)
		{
			goto IL_0055;
		}
	}

IL_0041:
	{
		Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0* L_10 = *(Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),2));
		il2cpp_codegen_memcpy(L_11, V_0, SizeOf_TSource_t21BF09076F270DC063711DE3ABB52B001A331F78);
		NullCheck(L_10);
		InvokerActionInvoker2< Il2CppFullySharedGenericAny, Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 10)), il2cpp_rgctx_method(method->klass->rgctx_data, 10), L_10, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->klass->rgctx_data, 8)) ? L_11: *(void**)L_11), (Il2CppFullySharedGenericAny*)L_12);
		il2cpp_codegen_write_instance_field_data(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 7),2), L_12, SizeOf_TResult_t278B55150BC17BB45D33B605F011F4D96EFE5425);
		return (bool)1;
	}

IL_0055:
	{
		int32_t L_13 = *(int32_t*)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),3));
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_14 = *(__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),0));
		NullCheck(L_14);
		if ((((int32_t)L_13) < ((int32_t)((int32_t)(((RuntimeArray*)L_14)->max_length)))))
		{
			goto IL_000b;
		}
	}
	{
		NullCheck((Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0*)__this);
		VirtualActionInvoker0Invoker::Invoke(12 /* System.Void System.Linq.Enumerable/Iterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::Dispose() */, (Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0*)__this);
	}

IL_006b:
	{
		return (bool)0;
	}
}
// System.Collections.Generic.IEnumerable`1<TResult> System.Linq.Enumerable/WhereSelectArrayIterator`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::Where(System.Func`2<TResult,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* WhereSelectArrayIterator_2_Where_mD81DB59B1D07BC8DDB099A652B22BA9C1538D7A3_gshared (/*System.Linq.Enumerable/WhereSelectArrayIterator`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>*/WhereSelectArrayIterator_2_tBE026CE497BB8F36E31685722BBD7CB567570174* __this, Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* ___predicate0, const RuntimeMethod* method) 
{
	{
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_0 = ___predicate0;
		WhereEnumerableIterator_1_t8B24528558F527941435C4FE1D046216FE4F277B* L_1 = (WhereEnumerableIterator_1_t8B24528558F527941435C4FE1D046216FE4F277B*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 14));
		NullCheck(L_1);
		InvokerActionInvoker2< RuntimeObject*, Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 15)), il2cpp_rgctx_method(method->klass->rgctx_data, 15), L_1, (RuntimeObject*)__this, L_0);
		return (RuntimeObject*)L_1;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::.ctor(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectEnumerableIterator_2__ctor_mA080BFD551E780DEE7C4C1CFE7BAAA6662478900_gshared (WhereSelectEnumerableIterator_2_t4A082C2E79F346CC9DA87508903869069C5C6454* __this, RuntimeObject* ___source0, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate1, Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B* ___selector2, const RuntimeMethod* method) 
{
	{
		Iterator_1__ctor_m27CEA132FC2E7D7BA6A8529CE0647F2709FC29A3((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 0));
		RuntimeObject* L_0 = ___source0;
		__this->___source_3 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___source_3), (void*)L_0);
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_1 = ___predicate1;
		__this->___predicate_4 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___predicate_4), (void*)L_1);
		Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B* L_2 = ___selector2;
		__this->___selector_5 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___selector_5), (void*)L_2);
		return;
	}
}
// System.Linq.Enumerable/Iterator`1<TResult> System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::Clone()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40* WhereSelectEnumerableIterator_2_Clone_mFBD855E424846C015FA79522BA6418F0C91973FE_gshared (WhereSelectEnumerableIterator_2_t4A082C2E79F346CC9DA87508903869069C5C6454* __this, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = (RuntimeObject*)__this->___source_3;
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_1 = (Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*)__this->___predicate_4;
		Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B* L_2 = (Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B*)__this->___selector_5;
		WhereSelectEnumerableIterator_2_t4A082C2E79F346CC9DA87508903869069C5C6454* L_3 = (WhereSelectEnumerableIterator_2_t4A082C2E79F346CC9DA87508903869069C5C6454*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 3));
		NullCheck(L_3);
		WhereSelectEnumerableIterator_2__ctor_mA080BFD551E780DEE7C4C1CFE7BAAA6662478900(L_3, L_0, L_1, L_2, il2cpp_rgctx_method(method->klass->rgctx_data, 6));
		return (Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)L_3;
	}
}
// System.Void System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::Dispose()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectEnumerableIterator_2_Dispose_mF4C5EFC89944DA01FE7D5FF5B6CA6DBC262F43B7_gshared (WhereSelectEnumerableIterator_2_t4A082C2E79F346CC9DA87508903869069C5C6454* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		RuntimeObject* L_0 = (RuntimeObject*)__this->___enumerator_6;
		if (!L_0)
		{
			goto IL_0013;
		}
	}
	{
		RuntimeObject* L_1 = (RuntimeObject*)__this->___enumerator_6;
		NullCheck((RuntimeObject*)L_1);
		InterfaceActionInvoker0::Invoke(0 /* System.Void System.IDisposable::Dispose() */, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, (RuntimeObject*)L_1);
	}

IL_0013:
	{
		__this->___enumerator_6 = (RuntimeObject*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___enumerator_6), (void*)(RuntimeObject*)NULL);
		NullCheck((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this);
		Iterator_1_Dispose_m51BD0D9CCB4CB032C42DA20F3C867462771689AB((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 9));
		return;
	}
}
// System.Boolean System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool WhereSelectEnumerableIterator_2_MoveNext_mEF3619B569217B836A4831BF4CDF962084761335_gshared (WhereSelectEnumerableIterator_2_t4A082C2E79F346CC9DA87508903869069C5C6454* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		int32_t L_0 = (int32_t)((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this)->___state_1;
		V_0 = L_0;
		int32_t L_1 = V_0;
		if ((((int32_t)L_1) == ((int32_t)1)))
		{
			goto IL_0011;
		}
	}
	{
		int32_t L_2 = V_0;
		if ((((int32_t)L_2) == ((int32_t)2)))
		{
			goto IL_0061;
		}
	}
	{
		goto IL_0074;
	}

IL_0011:
	{
		RuntimeObject* L_3 = (RuntimeObject*)__this->___source_3;
		NullCheck(L_3);
		RuntimeObject* L_4;
		L_4 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0 /* System.Collections.Generic.IEnumerator`1<T> System.Collections.Generic.IEnumerable`1<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>>::GetEnumerator() */, il2cpp_rgctx_data(method->klass->rgctx_data, 2), L_3);
		__this->___enumerator_6 = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___enumerator_6), (void*)L_4);
		((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this)->___state_1 = 2;
		goto IL_0061;
	}

IL_002b:
	{
		RuntimeObject* L_5 = (RuntimeObject*)__this->___enumerator_6;
		NullCheck(L_5);
		ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 L_6;
		L_6 = InterfaceFuncInvoker0< ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 >::Invoke(0 /* T System.Collections.Generic.IEnumerator`1<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>>::get_Current() */, il2cpp_rgctx_data(method->klass->rgctx_data, 8), L_5);
		V_1 = L_6;
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_7 = (Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*)__this->___predicate_4;
		if (!L_7)
		{
			goto IL_004d;
		}
	}
	{
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_8 = (Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*)__this->___predicate_4;
		ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 L_9 = V_1;
		NullCheck(L_8);
		bool L_10;
		L_10 = Func_2_Invoke_m12B4F5C7D4C30A85BD18305B0CDE8DF42312F402_inline(L_8, L_9, il2cpp_rgctx_method(method->klass->rgctx_data, 13));
		if (!L_10)
		{
			goto IL_0061;
		}
	}

IL_004d:
	{
		Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B* L_11 = (Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B*)__this->___selector_5;
		ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 L_12 = V_1;
		NullCheck(L_11);
		ValueTuple_2_t761B36235D5F97F497A1CBCD077DF3858814FADE L_13;
		L_13 = Func_2_Invoke_m3794F6EAED760CA17FC7A24439DC423AC6BF8FB9_inline(L_11, L_12, il2cpp_rgctx_method(method->klass->rgctx_data, 14));
		((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this)->___current_2 = L_13;
		Il2CppCodeGenWriteBarrier((void**)&(((&((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this)->___current_2))->___Item1_0), (void*)NULL);
		return (bool)1;
	}

IL_0061:
	{
		RuntimeObject* L_14 = (RuntimeObject*)__this->___enumerator_6;
		NullCheck((RuntimeObject*)L_14);
		bool L_15;
		L_15 = InterfaceFuncInvoker0< bool >::Invoke(0 /* System.Boolean System.Collections.IEnumerator::MoveNext() */, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, (RuntimeObject*)L_14);
		if (L_15)
		{
			goto IL_002b;
		}
	}
	{
		NullCheck((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this);
		VirtualActionInvoker0::Invoke(12 /* System.Void System.Linq.Enumerable/Iterator`1<System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::Dispose() */, (Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this);
	}

IL_0074:
	{
		return (bool)0;
	}
}
// System.Collections.Generic.IEnumerable`1<TResult> System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::Where(System.Func`2<TResult,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* WhereSelectEnumerableIterator_2_Where_m0760B2454F9FE6113B2F21FB1EE16EB74F5A45A1_gshared (WhereSelectEnumerableIterator_2_t4A082C2E79F346CC9DA87508903869069C5C6454* __this, Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B* ___predicate0, const RuntimeMethod* method) 
{
	{
		Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B* L_0 = ___predicate0;
		WhereEnumerableIterator_1_tB576B4EE76F70B81DDF11FA35260EC589825E7C5* L_1 = (WhereEnumerableIterator_1_tB576B4EE76F70B81DDF11FA35260EC589825E7C5*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 17));
		NullCheck(L_1);
		WhereEnumerableIterator_1__ctor_mC87BD639674AE32961C0AC5A5878F4144E7DA780(L_1, (RuntimeObject*)__this, L_0, il2cpp_rgctx_method(method->klass->rgctx_data, 18));
		return (RuntimeObject*)L_1;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Object>::.ctor(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectEnumerableIterator_2__ctor_mA9895FAF25F9C9A7894DD268E375875F8FBA6E76_gshared (WhereSelectEnumerableIterator_2_t27953926A9605639C866748EA3EF6801E63B5430* __this, RuntimeObject* ___source0, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate1, Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287* ___selector2, const RuntimeMethod* method) 
{
	{
		Iterator_1__ctor_m3E47867714E05673E54C6B73D9242F5FFADA1F63((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 0));
		RuntimeObject* L_0 = ___source0;
		__this->___source_3 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___source_3), (void*)L_0);
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_1 = ___predicate1;
		__this->___predicate_4 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___predicate_4), (void*)L_1);
		Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287* L_2 = ___selector2;
		__this->___selector_5 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___selector_5), (void*)L_2);
		return;
	}
}
// System.Linq.Enumerable/Iterator`1<TResult> System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Object>::Clone()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA* WhereSelectEnumerableIterator_2_Clone_mD5B870476E5E903669576306E70329B666F137F8_gshared (WhereSelectEnumerableIterator_2_t27953926A9605639C866748EA3EF6801E63B5430* __this, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = (RuntimeObject*)__this->___source_3;
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_1 = (Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*)__this->___predicate_4;
		Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287* L_2 = (Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287*)__this->___selector_5;
		WhereSelectEnumerableIterator_2_t27953926A9605639C866748EA3EF6801E63B5430* L_3 = (WhereSelectEnumerableIterator_2_t27953926A9605639C866748EA3EF6801E63B5430*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 3));
		NullCheck(L_3);
		WhereSelectEnumerableIterator_2__ctor_mA9895FAF25F9C9A7894DD268E375875F8FBA6E76(L_3, L_0, L_1, L_2, il2cpp_rgctx_method(method->klass->rgctx_data, 6));
		return (Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)L_3;
	}
}
// System.Void System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Object>::Dispose()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectEnumerableIterator_2_Dispose_mDA7A53AB55C5EB53552772426DAABF052167AAA8_gshared (WhereSelectEnumerableIterator_2_t27953926A9605639C866748EA3EF6801E63B5430* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		RuntimeObject* L_0 = (RuntimeObject*)__this->___enumerator_6;
		if (!L_0)
		{
			goto IL_0013;
		}
	}
	{
		RuntimeObject* L_1 = (RuntimeObject*)__this->___enumerator_6;
		NullCheck((RuntimeObject*)L_1);
		InterfaceActionInvoker0::Invoke(0 /* System.Void System.IDisposable::Dispose() */, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, (RuntimeObject*)L_1);
	}

IL_0013:
	{
		__this->___enumerator_6 = (RuntimeObject*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___enumerator_6), (void*)(RuntimeObject*)NULL);
		NullCheck((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this);
		Iterator_1_Dispose_m953BCF886C8A63456821023DBA45EBD9AC44EB07((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 9));
		return;
	}
}
// System.Boolean System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Object>::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool WhereSelectEnumerableIterator_2_MoveNext_mE301278799EF981E5F636870F421BFD4EE934CB0_gshared (WhereSelectEnumerableIterator_2_t27953926A9605639C866748EA3EF6801E63B5430* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		int32_t L_0 = (int32_t)((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this)->___state_1;
		V_0 = L_0;
		int32_t L_1 = V_0;
		if ((((int32_t)L_1) == ((int32_t)1)))
		{
			goto IL_0011;
		}
	}
	{
		int32_t L_2 = V_0;
		if ((((int32_t)L_2) == ((int32_t)2)))
		{
			goto IL_0061;
		}
	}
	{
		goto IL_0074;
	}

IL_0011:
	{
		RuntimeObject* L_3 = (RuntimeObject*)__this->___source_3;
		NullCheck(L_3);
		RuntimeObject* L_4;
		L_4 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0 /* System.Collections.Generic.IEnumerator`1<T> System.Collections.Generic.IEnumerable`1<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>>::GetEnumerator() */, il2cpp_rgctx_data(method->klass->rgctx_data, 2), L_3);
		__this->___enumerator_6 = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___enumerator_6), (void*)L_4);
		((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this)->___state_1 = 2;
		goto IL_0061;
	}

IL_002b:
	{
		RuntimeObject* L_5 = (RuntimeObject*)__this->___enumerator_6;
		NullCheck(L_5);
		ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 L_6;
		L_6 = InterfaceFuncInvoker0< ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 >::Invoke(0 /* T System.Collections.Generic.IEnumerator`1<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>>::get_Current() */, il2cpp_rgctx_data(method->klass->rgctx_data, 8), L_5);
		V_1 = L_6;
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_7 = (Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*)__this->___predicate_4;
		if (!L_7)
		{
			goto IL_004d;
		}
	}
	{
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_8 = (Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*)__this->___predicate_4;
		ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 L_9 = V_1;
		NullCheck(L_8);
		bool L_10;
		L_10 = Func_2_Invoke_m12B4F5C7D4C30A85BD18305B0CDE8DF42312F402_inline(L_8, L_9, il2cpp_rgctx_method(method->klass->rgctx_data, 13));
		if (!L_10)
		{
			goto IL_0061;
		}
	}

IL_004d:
	{
		Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287* L_11 = (Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287*)__this->___selector_5;
		ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 L_12 = V_1;
		NullCheck(L_11);
		RuntimeObject* L_13;
		L_13 = Func_2_Invoke_m9EB50B06293708FB57A5A30A460483868FFF99D0_inline(L_11, L_12, il2cpp_rgctx_method(method->klass->rgctx_data, 14));
		((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this)->___current_2 = L_13;
		Il2CppCodeGenWriteBarrier((void**)(&((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this)->___current_2), (void*)L_13);
		return (bool)1;
	}

IL_0061:
	{
		RuntimeObject* L_14 = (RuntimeObject*)__this->___enumerator_6;
		NullCheck((RuntimeObject*)L_14);
		bool L_15;
		L_15 = InterfaceFuncInvoker0< bool >::Invoke(0 /* System.Boolean System.Collections.IEnumerator::MoveNext() */, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, (RuntimeObject*)L_14);
		if (L_15)
		{
			goto IL_002b;
		}
	}
	{
		NullCheck((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this);
		VirtualActionInvoker0::Invoke(12 /* System.Void System.Linq.Enumerable/Iterator`1<System.Object>::Dispose() */, (Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this);
	}

IL_0074:
	{
		return (bool)0;
	}
}
// System.Collections.Generic.IEnumerable`1<TResult> System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Object>::Where(System.Func`2<TResult,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* WhereSelectEnumerableIterator_2_Where_mDB6F38F71355B540FB0E9DC9D77CEAC72B4D3D9F_gshared (WhereSelectEnumerableIterator_2_t27953926A9605639C866748EA3EF6801E63B5430* __this, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate0, const RuntimeMethod* method) 
{
	{
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_0 = ___predicate0;
		WhereEnumerableIterator_1_t1E787D13759F5A31C94B3FAED181402B25C278F4* L_1 = (WhereEnumerableIterator_1_t1E787D13759F5A31C94B3FAED181402B25C278F4*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 17));
		NullCheck(L_1);
		WhereEnumerableIterator_1__ctor_m8C0DA4CDA5431C03561F67C4393BB18CDD891F01(L_1, (RuntimeObject*)__this, L_0, il2cpp_rgctx_method(method->klass->rgctx_data, 18));
		return (RuntimeObject*)L_1;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Single>::.ctor(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectEnumerableIterator_2__ctor_m00D2CEA2D2B5A0C3A03E1A7A7F9BBA5229DEF1B2_gshared (WhereSelectEnumerableIterator_2_tA24C2F75772C5AB98B395DC9C70CDD82327A82C4* __this, RuntimeObject* ___source0, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate1, Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72* ___selector2, const RuntimeMethod* method) 
{
	{
		Iterator_1__ctor_m7FEBAE985CDE5DD1CB255EB4A3D65F05AA8D2AEF((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 0));
		RuntimeObject* L_0 = ___source0;
		__this->___source_3 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___source_3), (void*)L_0);
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_1 = ___predicate1;
		__this->___predicate_4 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___predicate_4), (void*)L_1);
		Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72* L_2 = ___selector2;
		__this->___selector_5 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___selector_5), (void*)L_2);
		return;
	}
}
// System.Linq.Enumerable/Iterator`1<TResult> System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Single>::Clone()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E* WhereSelectEnumerableIterator_2_Clone_mF72B8A8D0E6001477F7AA5AEAABAFFF9528C1DAD_gshared (WhereSelectEnumerableIterator_2_tA24C2F75772C5AB98B395DC9C70CDD82327A82C4* __this, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = (RuntimeObject*)__this->___source_3;
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_1 = (Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*)__this->___predicate_4;
		Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72* L_2 = (Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72*)__this->___selector_5;
		WhereSelectEnumerableIterator_2_tA24C2F75772C5AB98B395DC9C70CDD82327A82C4* L_3 = (WhereSelectEnumerableIterator_2_tA24C2F75772C5AB98B395DC9C70CDD82327A82C4*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 3));
		NullCheck(L_3);
		WhereSelectEnumerableIterator_2__ctor_m00D2CEA2D2B5A0C3A03E1A7A7F9BBA5229DEF1B2(L_3, L_0, L_1, L_2, il2cpp_rgctx_method(method->klass->rgctx_data, 6));
		return (Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)L_3;
	}
}
// System.Void System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Single>::Dispose()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectEnumerableIterator_2_Dispose_mDCF2F227323C012A48D36D57C8B06C6D9ACAD80E_gshared (WhereSelectEnumerableIterator_2_tA24C2F75772C5AB98B395DC9C70CDD82327A82C4* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		RuntimeObject* L_0 = (RuntimeObject*)__this->___enumerator_6;
		if (!L_0)
		{
			goto IL_0013;
		}
	}
	{
		RuntimeObject* L_1 = (RuntimeObject*)__this->___enumerator_6;
		NullCheck((RuntimeObject*)L_1);
		InterfaceActionInvoker0::Invoke(0 /* System.Void System.IDisposable::Dispose() */, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, (RuntimeObject*)L_1);
	}

IL_0013:
	{
		__this->___enumerator_6 = (RuntimeObject*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___enumerator_6), (void*)(RuntimeObject*)NULL);
		NullCheck((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this);
		Iterator_1_Dispose_mC0A88E7EEED5A9CFDCACF160DE8F58A713DFF222((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 9));
		return;
	}
}
// System.Boolean System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Single>::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool WhereSelectEnumerableIterator_2_MoveNext_mB1926D870789C033384B4B45F4D37858C248CEDE_gshared (WhereSelectEnumerableIterator_2_tA24C2F75772C5AB98B395DC9C70CDD82327A82C4* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		int32_t L_0 = (int32_t)((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this)->___state_1;
		V_0 = L_0;
		int32_t L_1 = V_0;
		if ((((int32_t)L_1) == ((int32_t)1)))
		{
			goto IL_0011;
		}
	}
	{
		int32_t L_2 = V_0;
		if ((((int32_t)L_2) == ((int32_t)2)))
		{
			goto IL_0061;
		}
	}
	{
		goto IL_0074;
	}

IL_0011:
	{
		RuntimeObject* L_3 = (RuntimeObject*)__this->___source_3;
		NullCheck(L_3);
		RuntimeObject* L_4;
		L_4 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0 /* System.Collections.Generic.IEnumerator`1<T> System.Collections.Generic.IEnumerable`1<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>>::GetEnumerator() */, il2cpp_rgctx_data(method->klass->rgctx_data, 2), L_3);
		__this->___enumerator_6 = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___enumerator_6), (void*)L_4);
		((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this)->___state_1 = 2;
		goto IL_0061;
	}

IL_002b:
	{
		RuntimeObject* L_5 = (RuntimeObject*)__this->___enumerator_6;
		NullCheck(L_5);
		ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 L_6;
		L_6 = InterfaceFuncInvoker0< ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 >::Invoke(0 /* T System.Collections.Generic.IEnumerator`1<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>>::get_Current() */, il2cpp_rgctx_data(method->klass->rgctx_data, 8), L_5);
		V_1 = L_6;
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_7 = (Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*)__this->___predicate_4;
		if (!L_7)
		{
			goto IL_004d;
		}
	}
	{
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_8 = (Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*)__this->___predicate_4;
		ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 L_9 = V_1;
		NullCheck(L_8);
		bool L_10;
		L_10 = Func_2_Invoke_m12B4F5C7D4C30A85BD18305B0CDE8DF42312F402_inline(L_8, L_9, il2cpp_rgctx_method(method->klass->rgctx_data, 13));
		if (!L_10)
		{
			goto IL_0061;
		}
	}

IL_004d:
	{
		Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72* L_11 = (Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72*)__this->___selector_5;
		ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 L_12 = V_1;
		NullCheck(L_11);
		float L_13;
		L_13 = Func_2_Invoke_mC888CBA13A68639AC714989A4F3DC91C432BB307_inline(L_11, L_12, il2cpp_rgctx_method(method->klass->rgctx_data, 14));
		((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this)->___current_2 = L_13;
		return (bool)1;
	}

IL_0061:
	{
		RuntimeObject* L_14 = (RuntimeObject*)__this->___enumerator_6;
		NullCheck((RuntimeObject*)L_14);
		bool L_15;
		L_15 = InterfaceFuncInvoker0< bool >::Invoke(0 /* System.Boolean System.Collections.IEnumerator::MoveNext() */, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, (RuntimeObject*)L_14);
		if (L_15)
		{
			goto IL_002b;
		}
	}
	{
		NullCheck((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this);
		VirtualActionInvoker0::Invoke(12 /* System.Void System.Linq.Enumerable/Iterator`1<System.Single>::Dispose() */, (Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this);
	}

IL_0074:
	{
		return (bool)0;
	}
}
// System.Collections.Generic.IEnumerable`1<TResult> System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Single>::Where(System.Func`2<TResult,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* WhereSelectEnumerableIterator_2_Where_m431678A2870718037934DB0145AE29E117E236F6_gshared (WhereSelectEnumerableIterator_2_tA24C2F75772C5AB98B395DC9C70CDD82327A82C4* __this, Func_2_t49E998685259ADE759F9329BF66F20DE8667006E* ___predicate0, const RuntimeMethod* method) 
{
	{
		Func_2_t49E998685259ADE759F9329BF66F20DE8667006E* L_0 = ___predicate0;
		WhereEnumerableIterator_1_t7BB2D1D9F8A6E52243A87F45DFEEA4209D685F7C* L_1 = (WhereEnumerableIterator_1_t7BB2D1D9F8A6E52243A87F45DFEEA4209D685F7C*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 17));
		NullCheck(L_1);
		WhereEnumerableIterator_1__ctor_mCB49A03958EF827EF6CE47402259941EAB31D984(L_1, (RuntimeObject*)__this, L_0, il2cpp_rgctx_method(method->klass->rgctx_data, 18));
		return (RuntimeObject*)L_1;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.Object,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::.ctor(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectEnumerableIterator_2__ctor_m5538389BC390D50DBDA10D3C0945A79D830641B3_gshared (WhereSelectEnumerableIterator_2_t2379CEC793B1983191566FBE7853007579556057* __this, RuntimeObject* ___source0, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate1, Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9* ___selector2, const RuntimeMethod* method) 
{
	{
		Iterator_1__ctor_m27CEA132FC2E7D7BA6A8529CE0647F2709FC29A3((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 0));
		RuntimeObject* L_0 = ___source0;
		__this->___source_3 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___source_3), (void*)L_0);
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_1 = ___predicate1;
		__this->___predicate_4 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___predicate_4), (void*)L_1);
		Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9* L_2 = ___selector2;
		__this->___selector_5 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___selector_5), (void*)L_2);
		return;
	}
}
// System.Linq.Enumerable/Iterator`1<TResult> System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.Object,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::Clone()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40* WhereSelectEnumerableIterator_2_Clone_m50338B94C488ACD4A8A284A03ECD845827AB8887_gshared (WhereSelectEnumerableIterator_2_t2379CEC793B1983191566FBE7853007579556057* __this, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = (RuntimeObject*)__this->___source_3;
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_1 = (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*)__this->___predicate_4;
		Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9* L_2 = (Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9*)__this->___selector_5;
		WhereSelectEnumerableIterator_2_t2379CEC793B1983191566FBE7853007579556057* L_3 = (WhereSelectEnumerableIterator_2_t2379CEC793B1983191566FBE7853007579556057*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 3));
		NullCheck(L_3);
		WhereSelectEnumerableIterator_2__ctor_m5538389BC390D50DBDA10D3C0945A79D830641B3(L_3, L_0, L_1, L_2, il2cpp_rgctx_method(method->klass->rgctx_data, 6));
		return (Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)L_3;
	}
}
// System.Void System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.Object,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::Dispose()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectEnumerableIterator_2_Dispose_m78104E5982388B1CF870700031B463E7BDC441E1_gshared (WhereSelectEnumerableIterator_2_t2379CEC793B1983191566FBE7853007579556057* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		RuntimeObject* L_0 = (RuntimeObject*)__this->___enumerator_6;
		if (!L_0)
		{
			goto IL_0013;
		}
	}
	{
		RuntimeObject* L_1 = (RuntimeObject*)__this->___enumerator_6;
		NullCheck((RuntimeObject*)L_1);
		InterfaceActionInvoker0::Invoke(0 /* System.Void System.IDisposable::Dispose() */, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, (RuntimeObject*)L_1);
	}

IL_0013:
	{
		__this->___enumerator_6 = (RuntimeObject*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___enumerator_6), (void*)(RuntimeObject*)NULL);
		NullCheck((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this);
		Iterator_1_Dispose_m51BD0D9CCB4CB032C42DA20F3C867462771689AB((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 9));
		return;
	}
}
// System.Boolean System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.Object,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool WhereSelectEnumerableIterator_2_MoveNext_mF4DE96D815DE8C1AFAD7584700547178DC6F8AEB_gshared (WhereSelectEnumerableIterator_2_t2379CEC793B1983191566FBE7853007579556057* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	RuntimeObject* V_1 = NULL;
	{
		int32_t L_0 = (int32_t)((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this)->___state_1;
		V_0 = L_0;
		int32_t L_1 = V_0;
		if ((((int32_t)L_1) == ((int32_t)1)))
		{
			goto IL_0011;
		}
	}
	{
		int32_t L_2 = V_0;
		if ((((int32_t)L_2) == ((int32_t)2)))
		{
			goto IL_0061;
		}
	}
	{
		goto IL_0074;
	}

IL_0011:
	{
		RuntimeObject* L_3 = (RuntimeObject*)__this->___source_3;
		NullCheck(L_3);
		RuntimeObject* L_4;
		L_4 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0 /* System.Collections.Generic.IEnumerator`1<T> System.Collections.Generic.IEnumerable`1<System.Object>::GetEnumerator() */, il2cpp_rgctx_data(method->klass->rgctx_data, 2), L_3);
		__this->___enumerator_6 = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___enumerator_6), (void*)L_4);
		((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this)->___state_1 = 2;
		goto IL_0061;
	}

IL_002b:
	{
		RuntimeObject* L_5 = (RuntimeObject*)__this->___enumerator_6;
		NullCheck(L_5);
		RuntimeObject* L_6;
		L_6 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0 /* T System.Collections.Generic.IEnumerator`1<System.Object>::get_Current() */, il2cpp_rgctx_data(method->klass->rgctx_data, 8), L_5);
		V_1 = L_6;
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_7 = (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*)__this->___predicate_4;
		if (!L_7)
		{
			goto IL_004d;
		}
	}
	{
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_8 = (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*)__this->___predicate_4;
		RuntimeObject* L_9 = V_1;
		NullCheck(L_8);
		bool L_10;
		L_10 = Func_2_Invoke_m2014423FB900F135C8FF994125604FF9E6AAE829_inline(L_8, L_9, il2cpp_rgctx_method(method->klass->rgctx_data, 13));
		if (!L_10)
		{
			goto IL_0061;
		}
	}

IL_004d:
	{
		Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9* L_11 = (Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9*)__this->___selector_5;
		RuntimeObject* L_12 = V_1;
		NullCheck(L_11);
		ValueTuple_2_t761B36235D5F97F497A1CBCD077DF3858814FADE L_13;
		L_13 = Func_2_Invoke_m30EE337C21F1CA674046A64A8A2DA68A02701634_inline(L_11, L_12, il2cpp_rgctx_method(method->klass->rgctx_data, 14));
		((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this)->___current_2 = L_13;
		Il2CppCodeGenWriteBarrier((void**)&(((&((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this)->___current_2))->___Item1_0), (void*)NULL);
		return (bool)1;
	}

IL_0061:
	{
		RuntimeObject* L_14 = (RuntimeObject*)__this->___enumerator_6;
		NullCheck((RuntimeObject*)L_14);
		bool L_15;
		L_15 = InterfaceFuncInvoker0< bool >::Invoke(0 /* System.Boolean System.Collections.IEnumerator::MoveNext() */, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, (RuntimeObject*)L_14);
		if (L_15)
		{
			goto IL_002b;
		}
	}
	{
		NullCheck((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this);
		VirtualActionInvoker0::Invoke(12 /* System.Void System.Linq.Enumerable/Iterator`1<System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::Dispose() */, (Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this);
	}

IL_0074:
	{
		return (bool)0;
	}
}
// System.Collections.Generic.IEnumerable`1<TResult> System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.Object,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::Where(System.Func`2<TResult,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* WhereSelectEnumerableIterator_2_Where_mFB88FF4097E7B8B33E5907C883AF9B613B01C3CA_gshared (WhereSelectEnumerableIterator_2_t2379CEC793B1983191566FBE7853007579556057* __this, Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B* ___predicate0, const RuntimeMethod* method) 
{
	{
		Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B* L_0 = ___predicate0;
		WhereEnumerableIterator_1_tB576B4EE76F70B81DDF11FA35260EC589825E7C5* L_1 = (WhereEnumerableIterator_1_tB576B4EE76F70B81DDF11FA35260EC589825E7C5*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 17));
		NullCheck(L_1);
		WhereEnumerableIterator_1__ctor_mC87BD639674AE32961C0AC5A5878F4144E7DA780(L_1, (RuntimeObject*)__this, L_0, il2cpp_rgctx_method(method->klass->rgctx_data, 18));
		return (RuntimeObject*)L_1;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.Object,System.Single>::.ctor(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectEnumerableIterator_2__ctor_mE8B29F90A4CD398E256C48F9451231F2EE02008E_gshared (WhereSelectEnumerableIterator_2_tF7B17AE20C591499CCC69CF4CFEA521F28D66E94* __this, RuntimeObject* ___source0, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate1, Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12* ___selector2, const RuntimeMethod* method) 
{
	{
		Iterator_1__ctor_m7FEBAE985CDE5DD1CB255EB4A3D65F05AA8D2AEF((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 0));
		RuntimeObject* L_0 = ___source0;
		__this->___source_3 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___source_3), (void*)L_0);
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_1 = ___predicate1;
		__this->___predicate_4 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___predicate_4), (void*)L_1);
		Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12* L_2 = ___selector2;
		__this->___selector_5 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___selector_5), (void*)L_2);
		return;
	}
}
// System.Linq.Enumerable/Iterator`1<TResult> System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.Object,System.Single>::Clone()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E* WhereSelectEnumerableIterator_2_Clone_mD3C803B488F94D24D9F129FD93E41274437E991E_gshared (WhereSelectEnumerableIterator_2_tF7B17AE20C591499CCC69CF4CFEA521F28D66E94* __this, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = (RuntimeObject*)__this->___source_3;
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_1 = (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*)__this->___predicate_4;
		Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12* L_2 = (Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12*)__this->___selector_5;
		WhereSelectEnumerableIterator_2_tF7B17AE20C591499CCC69CF4CFEA521F28D66E94* L_3 = (WhereSelectEnumerableIterator_2_tF7B17AE20C591499CCC69CF4CFEA521F28D66E94*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 3));
		NullCheck(L_3);
		WhereSelectEnumerableIterator_2__ctor_mE8B29F90A4CD398E256C48F9451231F2EE02008E(L_3, L_0, L_1, L_2, il2cpp_rgctx_method(method->klass->rgctx_data, 6));
		return (Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)L_3;
	}
}
// System.Void System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.Object,System.Single>::Dispose()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectEnumerableIterator_2_Dispose_mC03D128C0216636BDA33DD30E366835164B2FEC3_gshared (WhereSelectEnumerableIterator_2_tF7B17AE20C591499CCC69CF4CFEA521F28D66E94* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		RuntimeObject* L_0 = (RuntimeObject*)__this->___enumerator_6;
		if (!L_0)
		{
			goto IL_0013;
		}
	}
	{
		RuntimeObject* L_1 = (RuntimeObject*)__this->___enumerator_6;
		NullCheck((RuntimeObject*)L_1);
		InterfaceActionInvoker0::Invoke(0 /* System.Void System.IDisposable::Dispose() */, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, (RuntimeObject*)L_1);
	}

IL_0013:
	{
		__this->___enumerator_6 = (RuntimeObject*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___enumerator_6), (void*)(RuntimeObject*)NULL);
		NullCheck((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this);
		Iterator_1_Dispose_mC0A88E7EEED5A9CFDCACF160DE8F58A713DFF222((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 9));
		return;
	}
}
// System.Boolean System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.Object,System.Single>::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool WhereSelectEnumerableIterator_2_MoveNext_mC191409559A19D8A9E50612452F42848CBE42A9E_gshared (WhereSelectEnumerableIterator_2_tF7B17AE20C591499CCC69CF4CFEA521F28D66E94* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	RuntimeObject* V_1 = NULL;
	{
		int32_t L_0 = (int32_t)((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this)->___state_1;
		V_0 = L_0;
		int32_t L_1 = V_0;
		if ((((int32_t)L_1) == ((int32_t)1)))
		{
			goto IL_0011;
		}
	}
	{
		int32_t L_2 = V_0;
		if ((((int32_t)L_2) == ((int32_t)2)))
		{
			goto IL_0061;
		}
	}
	{
		goto IL_0074;
	}

IL_0011:
	{
		RuntimeObject* L_3 = (RuntimeObject*)__this->___source_3;
		NullCheck(L_3);
		RuntimeObject* L_4;
		L_4 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0 /* System.Collections.Generic.IEnumerator`1<T> System.Collections.Generic.IEnumerable`1<System.Object>::GetEnumerator() */, il2cpp_rgctx_data(method->klass->rgctx_data, 2), L_3);
		__this->___enumerator_6 = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___enumerator_6), (void*)L_4);
		((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this)->___state_1 = 2;
		goto IL_0061;
	}

IL_002b:
	{
		RuntimeObject* L_5 = (RuntimeObject*)__this->___enumerator_6;
		NullCheck(L_5);
		RuntimeObject* L_6;
		L_6 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0 /* T System.Collections.Generic.IEnumerator`1<System.Object>::get_Current() */, il2cpp_rgctx_data(method->klass->rgctx_data, 8), L_5);
		V_1 = L_6;
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_7 = (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*)__this->___predicate_4;
		if (!L_7)
		{
			goto IL_004d;
		}
	}
	{
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_8 = (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*)__this->___predicate_4;
		RuntimeObject* L_9 = V_1;
		NullCheck(L_8);
		bool L_10;
		L_10 = Func_2_Invoke_m2014423FB900F135C8FF994125604FF9E6AAE829_inline(L_8, L_9, il2cpp_rgctx_method(method->klass->rgctx_data, 13));
		if (!L_10)
		{
			goto IL_0061;
		}
	}

IL_004d:
	{
		Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12* L_11 = (Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12*)__this->___selector_5;
		RuntimeObject* L_12 = V_1;
		NullCheck(L_11);
		float L_13;
		L_13 = Func_2_Invoke_mBE16A5FDA5E80CCBA51D69334EF21C0F03D353AF_inline(L_11, L_12, il2cpp_rgctx_method(method->klass->rgctx_data, 14));
		((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this)->___current_2 = L_13;
		return (bool)1;
	}

IL_0061:
	{
		RuntimeObject* L_14 = (RuntimeObject*)__this->___enumerator_6;
		NullCheck((RuntimeObject*)L_14);
		bool L_15;
		L_15 = InterfaceFuncInvoker0< bool >::Invoke(0 /* System.Boolean System.Collections.IEnumerator::MoveNext() */, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, (RuntimeObject*)L_14);
		if (L_15)
		{
			goto IL_002b;
		}
	}
	{
		NullCheck((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this);
		VirtualActionInvoker0::Invoke(12 /* System.Void System.Linq.Enumerable/Iterator`1<System.Single>::Dispose() */, (Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this);
	}

IL_0074:
	{
		return (bool)0;
	}
}
// System.Collections.Generic.IEnumerable`1<TResult> System.Linq.Enumerable/WhereSelectEnumerableIterator`2<System.Object,System.Single>::Where(System.Func`2<TResult,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* WhereSelectEnumerableIterator_2_Where_mD90BB6C0B5510E39C91CC55D6DCA11BA47E9298C_gshared (WhereSelectEnumerableIterator_2_tF7B17AE20C591499CCC69CF4CFEA521F28D66E94* __this, Func_2_t49E998685259ADE759F9329BF66F20DE8667006E* ___predicate0, const RuntimeMethod* method) 
{
	{
		Func_2_t49E998685259ADE759F9329BF66F20DE8667006E* L_0 = ___predicate0;
		WhereEnumerableIterator_1_t7BB2D1D9F8A6E52243A87F45DFEEA4209D685F7C* L_1 = (WhereEnumerableIterator_1_t7BB2D1D9F8A6E52243A87F45DFEEA4209D685F7C*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 17));
		NullCheck(L_1);
		WhereEnumerableIterator_1__ctor_mCB49A03958EF827EF6CE47402259941EAB31D984(L_1, (RuntimeObject*)__this, L_0, il2cpp_rgctx_method(method->klass->rgctx_data, 18));
		return (RuntimeObject*)L_1;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void System.Linq.Enumerable/WhereSelectEnumerableIterator`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::.ctor(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectEnumerableIterator_2__ctor_m9A4AF54DC527FA1CEF8B803C8DDA5E632838B06F_gshared (/*System.Linq.Enumerable/WhereSelectEnumerableIterator`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>*/WhereSelectEnumerableIterator_2_t1FBA58379B31F544881FB4C45B2D102F32A71E1C* __this, RuntimeObject* ___source0, Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* ___predicate1, Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0* ___selector2, const RuntimeMethod* method) 
{
	{
		InvokerActionInvoker0::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 0)), il2cpp_rgctx_method(method->klass->rgctx_data, 0), (Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0*)__this);
		RuntimeObject* L_0 = ___source0;
		il2cpp_codegen_write_instance_field_data<RuntimeObject*>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),0), L_0);
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_1 = ___predicate1;
		il2cpp_codegen_write_instance_field_data<Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B*>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),1), L_1);
		Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0* L_2 = ___selector2;
		il2cpp_codegen_write_instance_field_data<Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0*>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),2), L_2);
		return;
	}
}
// System.Linq.Enumerable/Iterator`1<TResult> System.Linq.Enumerable/WhereSelectEnumerableIterator`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::Clone()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0* WhereSelectEnumerableIterator_2_Clone_mD773B8B24D1459B11BA4462A6DD68865514ADC9E_gshared (/*System.Linq.Enumerable/WhereSelectEnumerableIterator`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>*/WhereSelectEnumerableIterator_2_t1FBA58379B31F544881FB4C45B2D102F32A71E1C* __this, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = *(RuntimeObject**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),0));
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_1 = *(Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),1));
		Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0* L_2 = *(Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),2));
		WhereSelectEnumerableIterator_2_t1FBA58379B31F544881FB4C45B2D102F32A71E1C* L_3 = (WhereSelectEnumerableIterator_2_t1FBA58379B31F544881FB4C45B2D102F32A71E1C*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 3));
		NullCheck(L_3);
		InvokerActionInvoker3< RuntimeObject*, Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B*, Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 6)), il2cpp_rgctx_method(method->klass->rgctx_data, 6), L_3, L_0, L_1, L_2);
		return (Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0*)L_3;
	}
}
// System.Void System.Linq.Enumerable/WhereSelectEnumerableIterator`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::Dispose()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectEnumerableIterator_2_Dispose_m640FAC111BC786414B40480BB03E4F84B2FFB179_gshared (/*System.Linq.Enumerable/WhereSelectEnumerableIterator`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>*/WhereSelectEnumerableIterator_2_t1FBA58379B31F544881FB4C45B2D102F32A71E1C* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		RuntimeObject* L_0 = *(RuntimeObject**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),3));
		if (!L_0)
		{
			goto IL_0013;
		}
	}
	{
		RuntimeObject* L_1 = *(RuntimeObject**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),3));
		NullCheck((RuntimeObject*)L_1);
		InterfaceActionInvoker0::Invoke(0 /* System.Void System.IDisposable::Dispose() */, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, (RuntimeObject*)L_1);
	}

IL_0013:
	{
		il2cpp_codegen_write_instance_field_data<RuntimeObject*>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),3), (RuntimeObject*)NULL);
		NullCheck((Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0*)__this);
		InvokerActionInvoker0::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 9)), il2cpp_rgctx_method(method->klass->rgctx_data, 9), (Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0*)__this);
		return;
	}
}
// System.Boolean System.Linq.Enumerable/WhereSelectEnumerableIterator`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool WhereSelectEnumerableIterator_2_MoveNext_mB384EFAF6366166F28EDFDBA272EEC1089E1A115_gshared (/*System.Linq.Enumerable/WhereSelectEnumerableIterator`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>*/WhereSelectEnumerableIterator_2_t1FBA58379B31F544881FB4C45B2D102F32A71E1C* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	// sizeof(TSource)
	const uint32_t SizeOf_TSource_t5B0D27614F68D07DB050466831DEDC1DDEFFC093 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->klass->rgctx_data, 12));
	// sizeof(TResult)
	const uint32_t SizeOf_TResult_t33CDF94D13BEBA6908E84F958D63A95F7466E520 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->klass->rgctx_data, 15));
	// TSource
	const Il2CppFullySharedGenericAny L_6 = alloca(SizeOf_TSource_t5B0D27614F68D07DB050466831DEDC1DDEFFC093);
	const Il2CppFullySharedGenericAny L_9 = L_6;
	const Il2CppFullySharedGenericAny L_12 = L_6;
	// TResult
	const Il2CppFullySharedGenericAny L_13 = alloca(SizeOf_TResult_t33CDF94D13BEBA6908E84F958D63A95F7466E520);
	int32_t V_0 = 0;
	Il2CppFullySharedGenericAny V_1 = alloca(SizeOf_TSource_t5B0D27614F68D07DB050466831DEDC1DDEFFC093);
	memset(V_1, 0, SizeOf_TSource_t5B0D27614F68D07DB050466831DEDC1DDEFFC093);
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 7),1));
		V_0 = L_0;
		int32_t L_1 = V_0;
		if ((((int32_t)L_1) == ((int32_t)1)))
		{
			goto IL_0011;
		}
	}
	{
		int32_t L_2 = V_0;
		if ((((int32_t)L_2) == ((int32_t)2)))
		{
			goto IL_0061;
		}
	}
	{
		goto IL_0074;
	}

IL_0011:
	{
		RuntimeObject* L_3 = *(RuntimeObject**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),0));
		NullCheck(L_3);
		RuntimeObject* L_4;
		L_4 = InterfaceFuncInvoker0Invoker< RuntimeObject* >::Invoke(0 /* System.Collections.Generic.IEnumerator`1<T> System.Collections.Generic.IEnumerable`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::GetEnumerator() */, il2cpp_rgctx_data(method->klass->rgctx_data, 2), L_3);
		il2cpp_codegen_write_instance_field_data<RuntimeObject*>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),3), L_4);
		il2cpp_codegen_write_instance_field_data<int32_t>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 7),1), 2);
		goto IL_0061;
	}

IL_002b:
	{
		RuntimeObject* L_5 = *(RuntimeObject**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),3));
		NullCheck(L_5);
		InterfaceActionInvoker1Invoker< Il2CppFullySharedGenericAny* >::Invoke(0 /* T System.Collections.Generic.IEnumerator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::get_Current() */, il2cpp_rgctx_data(method->klass->rgctx_data, 8), L_5, (Il2CppFullySharedGenericAny*)L_6);
		il2cpp_codegen_memcpy(V_1, L_6, SizeOf_TSource_t5B0D27614F68D07DB050466831DEDC1DDEFFC093);
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_7 = *(Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),1));
		if (!L_7)
		{
			goto IL_004d;
		}
	}
	{
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_8 = *(Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),1));
		il2cpp_codegen_memcpy(L_9, V_1, SizeOf_TSource_t5B0D27614F68D07DB050466831DEDC1DDEFFC093);
		NullCheck(L_8);
		bool L_10;
		L_10 = InvokerFuncInvoker1< bool, Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 13)), il2cpp_rgctx_method(method->klass->rgctx_data, 13), L_8, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->klass->rgctx_data, 12)) ? L_9: *(void**)L_9));
		if (!L_10)
		{
			goto IL_0061;
		}
	}

IL_004d:
	{
		Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0* L_11 = *(Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),2));
		il2cpp_codegen_memcpy(L_12, V_1, SizeOf_TSource_t5B0D27614F68D07DB050466831DEDC1DDEFFC093);
		NullCheck(L_11);
		InvokerActionInvoker2< Il2CppFullySharedGenericAny, Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 14)), il2cpp_rgctx_method(method->klass->rgctx_data, 14), L_11, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->klass->rgctx_data, 12)) ? L_12: *(void**)L_12), (Il2CppFullySharedGenericAny*)L_13);
		il2cpp_codegen_write_instance_field_data(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 7),2), L_13, SizeOf_TResult_t33CDF94D13BEBA6908E84F958D63A95F7466E520);
		return (bool)1;
	}

IL_0061:
	{
		RuntimeObject* L_14 = *(RuntimeObject**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),3));
		NullCheck((RuntimeObject*)L_14);
		bool L_15;
		L_15 = InterfaceFuncInvoker0< bool >::Invoke(0 /* System.Boolean System.Collections.IEnumerator::MoveNext() */, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, (RuntimeObject*)L_14);
		if (L_15)
		{
			goto IL_002b;
		}
	}
	{
		NullCheck((Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0*)__this);
		VirtualActionInvoker0Invoker::Invoke(12 /* System.Void System.Linq.Enumerable/Iterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::Dispose() */, (Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0*)__this);
	}

IL_0074:
	{
		return (bool)0;
	}
}
// System.Collections.Generic.IEnumerable`1<TResult> System.Linq.Enumerable/WhereSelectEnumerableIterator`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::Where(System.Func`2<TResult,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* WhereSelectEnumerableIterator_2_Where_mB8ACBBFA48460E67B18647EF16E6EE4D0BE08679_gshared (/*System.Linq.Enumerable/WhereSelectEnumerableIterator`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>*/WhereSelectEnumerableIterator_2_t1FBA58379B31F544881FB4C45B2D102F32A71E1C* __this, Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* ___predicate0, const RuntimeMethod* method) 
{
	{
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_0 = ___predicate0;
		WhereEnumerableIterator_1_t8B24528558F527941435C4FE1D046216FE4F277B* L_1 = (WhereEnumerableIterator_1_t8B24528558F527941435C4FE1D046216FE4F277B*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 17));
		NullCheck(L_1);
		InvokerActionInvoker2< RuntimeObject*, Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 18)), il2cpp_rgctx_method(method->klass->rgctx_data, 18), L_1, (RuntimeObject*)__this, L_0);
		return (RuntimeObject*)L_1;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void System.Linq.Enumerable/WhereSelectListIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::.ctor(System.Collections.Generic.List`1<TSource>,System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectListIterator_2__ctor_m8AEB85EB9B265A2F69C0BE789DA26BA006C9E903_gshared (WhereSelectListIterator_2_t8F14DC7F95888AA905CF6AE27A6AFC1205DC2933* __this, List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE* ___source0, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate1, Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B* ___selector2, const RuntimeMethod* method) 
{
	{
		Iterator_1__ctor_m27CEA132FC2E7D7BA6A8529CE0647F2709FC29A3((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 0));
		List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE* L_0 = ___source0;
		__this->___source_3 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___source_3), (void*)L_0);
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_1 = ___predicate1;
		__this->___predicate_4 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___predicate_4), (void*)L_1);
		Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B* L_2 = ___selector2;
		__this->___selector_5 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___selector_5), (void*)L_2);
		return;
	}
}
// System.Linq.Enumerable/Iterator`1<TResult> System.Linq.Enumerable/WhereSelectListIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::Clone()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40* WhereSelectListIterator_2_Clone_m9BC8EEA16C0E6DCA5185B869B625609CDECDE439_gshared (WhereSelectListIterator_2_t8F14DC7F95888AA905CF6AE27A6AFC1205DC2933* __this, const RuntimeMethod* method) 
{
	{
		List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE* L_0 = (List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE*)__this->___source_3;
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_1 = (Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*)__this->___predicate_4;
		Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B* L_2 = (Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B*)__this->___selector_5;
		WhereSelectListIterator_2_t8F14DC7F95888AA905CF6AE27A6AFC1205DC2933* L_3 = (WhereSelectListIterator_2_t8F14DC7F95888AA905CF6AE27A6AFC1205DC2933*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 3));
		NullCheck(L_3);
		WhereSelectListIterator_2__ctor_m8AEB85EB9B265A2F69C0BE789DA26BA006C9E903(L_3, L_0, L_1, L_2, il2cpp_rgctx_method(method->klass->rgctx_data, 6));
		return (Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)L_3;
	}
}
// System.Boolean System.Linq.Enumerable/WhereSelectListIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool WhereSelectListIterator_2_MoveNext_m46F02E744D0B6B2D0EEDE2D4A3CDDFEA267D868B_gshared (WhereSelectListIterator_2_t8F14DC7F95888AA905CF6AE27A6AFC1205DC2933* __this, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		int32_t L_0 = (int32_t)((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this)->___state_1;
		V_0 = L_0;
		int32_t L_1 = V_0;
		if ((((int32_t)L_1) == ((int32_t)1)))
		{
			goto IL_0011;
		}
	}
	{
		int32_t L_2 = V_0;
		if ((((int32_t)L_2) == ((int32_t)2)))
		{
			goto IL_0061;
		}
	}
	{
		goto IL_0074;
	}

IL_0011:
	{
		List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE* L_3 = (List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE*)__this->___source_3;
		NullCheck(L_3);
		Enumerator_t3ACCE1C7605054D2EF26F63B987FBD7CEA380901 L_4;
		L_4 = List_1_GetEnumerator_m180F98F72F5DA91BF8696E010117148062025D82(L_3, il2cpp_rgctx_method(method->klass->rgctx_data, 8));
		__this->___enumerator_6 = L_4;
		Il2CppCodeGenWriteBarrier((void**)&(((&__this->___enumerator_6))->____list_0), (void*)NULL);
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&(((&__this->___enumerator_6))->____current_3))->___Item1_0), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&((&(((&__this->___enumerator_6))->____current_3))->___Item2_1))->___rigidbody_2), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&((&(((&__this->___enumerator_6))->____current_3))->___Item2_1))->___collider_4), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&((&(((&__this->___enumerator_6))->____current_3))->___Item2_1))->___colliders_5), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&((&(((&__this->___enumerator_6))->____current_3))->___Item3_2))->___rigidbody_2), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&((&(((&__this->___enumerator_6))->____current_3))->___Item3_2))->___collider_4), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&((&(((&__this->___enumerator_6))->____current_3))->___Item3_2))->___colliders_5), (void*)NULL);
		#endif
		((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this)->___state_1 = 2;
		goto IL_0061;
	}

IL_002b:
	{
		Enumerator_t3ACCE1C7605054D2EF26F63B987FBD7CEA380901* L_5 = (Enumerator_t3ACCE1C7605054D2EF26F63B987FBD7CEA380901*)(&__this->___enumerator_6);
		ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 L_6;
		L_6 = Enumerator_get_Current_mE43C1A4AD61C4A5D58C7E5F6D98CD7DE40A64987_inline(L_5, il2cpp_rgctx_method(method->klass->rgctx_data, 10));
		V_1 = L_6;
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_7 = (Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*)__this->___predicate_4;
		if (!L_7)
		{
			goto IL_004d;
		}
	}
	{
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_8 = (Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*)__this->___predicate_4;
		ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 L_9 = V_1;
		NullCheck(L_8);
		bool L_10;
		L_10 = Func_2_Invoke_m12B4F5C7D4C30A85BD18305B0CDE8DF42312F402_inline(L_8, L_9, il2cpp_rgctx_method(method->klass->rgctx_data, 12));
		if (!L_10)
		{
			goto IL_0061;
		}
	}

IL_004d:
	{
		Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B* L_11 = (Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B*)__this->___selector_5;
		ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 L_12 = V_1;
		NullCheck(L_11);
		ValueTuple_2_t761B36235D5F97F497A1CBCD077DF3858814FADE L_13;
		L_13 = Func_2_Invoke_m3794F6EAED760CA17FC7A24439DC423AC6BF8FB9_inline(L_11, L_12, il2cpp_rgctx_method(method->klass->rgctx_data, 13));
		((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this)->___current_2 = L_13;
		Il2CppCodeGenWriteBarrier((void**)&(((&((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this)->___current_2))->___Item1_0), (void*)NULL);
		return (bool)1;
	}

IL_0061:
	{
		Enumerator_t3ACCE1C7605054D2EF26F63B987FBD7CEA380901* L_14 = (Enumerator_t3ACCE1C7605054D2EF26F63B987FBD7CEA380901*)(&__this->___enumerator_6);
		bool L_15;
		L_15 = Enumerator_MoveNext_mD0B6A0F10BA08A58F39285C8C8309111A79A3F7A(L_14, il2cpp_rgctx_method(method->klass->rgctx_data, 15));
		if (L_15)
		{
			goto IL_002b;
		}
	}
	{
		NullCheck((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this);
		VirtualActionInvoker0::Invoke(12 /* System.Void System.Linq.Enumerable/Iterator`1<System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::Dispose() */, (Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this);
	}

IL_0074:
	{
		return (bool)0;
	}
}
// System.Collections.Generic.IEnumerable`1<TResult> System.Linq.Enumerable/WhereSelectListIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::Where(System.Func`2<TResult,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* WhereSelectListIterator_2_Where_mA31AA0CD6142897FDB9F09E703F1999054508ADE_gshared (WhereSelectListIterator_2_t8F14DC7F95888AA905CF6AE27A6AFC1205DC2933* __this, Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B* ___predicate0, const RuntimeMethod* method) 
{
	{
		Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B* L_0 = ___predicate0;
		WhereEnumerableIterator_1_tB576B4EE76F70B81DDF11FA35260EC589825E7C5* L_1 = (WhereEnumerableIterator_1_tB576B4EE76F70B81DDF11FA35260EC589825E7C5*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 18));
		NullCheck(L_1);
		WhereEnumerableIterator_1__ctor_mC87BD639674AE32961C0AC5A5878F4144E7DA780(L_1, (RuntimeObject*)__this, L_0, il2cpp_rgctx_method(method->klass->rgctx_data, 19));
		return (RuntimeObject*)L_1;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void System.Linq.Enumerable/WhereSelectListIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Object>::.ctor(System.Collections.Generic.List`1<TSource>,System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectListIterator_2__ctor_mB2F43B2048C674205F65D38CD3AD6FE74C08FBE0_gshared (WhereSelectListIterator_2_tEB04EFC71ED85DC1E14F1FC388A8CCAF2FB25E8B* __this, List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE* ___source0, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate1, Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287* ___selector2, const RuntimeMethod* method) 
{
	{
		Iterator_1__ctor_m3E47867714E05673E54C6B73D9242F5FFADA1F63((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 0));
		List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE* L_0 = ___source0;
		__this->___source_3 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___source_3), (void*)L_0);
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_1 = ___predicate1;
		__this->___predicate_4 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___predicate_4), (void*)L_1);
		Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287* L_2 = ___selector2;
		__this->___selector_5 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___selector_5), (void*)L_2);
		return;
	}
}
// System.Linq.Enumerable/Iterator`1<TResult> System.Linq.Enumerable/WhereSelectListIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Object>::Clone()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA* WhereSelectListIterator_2_Clone_mB1756EC7AF7FBB17DCFD53B8E28612580B9FA9F5_gshared (WhereSelectListIterator_2_tEB04EFC71ED85DC1E14F1FC388A8CCAF2FB25E8B* __this, const RuntimeMethod* method) 
{
	{
		List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE* L_0 = (List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE*)__this->___source_3;
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_1 = (Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*)__this->___predicate_4;
		Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287* L_2 = (Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287*)__this->___selector_5;
		WhereSelectListIterator_2_tEB04EFC71ED85DC1E14F1FC388A8CCAF2FB25E8B* L_3 = (WhereSelectListIterator_2_tEB04EFC71ED85DC1E14F1FC388A8CCAF2FB25E8B*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 3));
		NullCheck(L_3);
		WhereSelectListIterator_2__ctor_mB2F43B2048C674205F65D38CD3AD6FE74C08FBE0(L_3, L_0, L_1, L_2, il2cpp_rgctx_method(method->klass->rgctx_data, 6));
		return (Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)L_3;
	}
}
// System.Boolean System.Linq.Enumerable/WhereSelectListIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Object>::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool WhereSelectListIterator_2_MoveNext_m0EDCC4B62E5900E9E1AD1480197660CD625C39EC_gshared (WhereSelectListIterator_2_tEB04EFC71ED85DC1E14F1FC388A8CCAF2FB25E8B* __this, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		int32_t L_0 = (int32_t)((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this)->___state_1;
		V_0 = L_0;
		int32_t L_1 = V_0;
		if ((((int32_t)L_1) == ((int32_t)1)))
		{
			goto IL_0011;
		}
	}
	{
		int32_t L_2 = V_0;
		if ((((int32_t)L_2) == ((int32_t)2)))
		{
			goto IL_0061;
		}
	}
	{
		goto IL_0074;
	}

IL_0011:
	{
		List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE* L_3 = (List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE*)__this->___source_3;
		NullCheck(L_3);
		Enumerator_t3ACCE1C7605054D2EF26F63B987FBD7CEA380901 L_4;
		L_4 = List_1_GetEnumerator_m180F98F72F5DA91BF8696E010117148062025D82(L_3, il2cpp_rgctx_method(method->klass->rgctx_data, 8));
		__this->___enumerator_6 = L_4;
		Il2CppCodeGenWriteBarrier((void**)&(((&__this->___enumerator_6))->____list_0), (void*)NULL);
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&(((&__this->___enumerator_6))->____current_3))->___Item1_0), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&((&(((&__this->___enumerator_6))->____current_3))->___Item2_1))->___rigidbody_2), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&((&(((&__this->___enumerator_6))->____current_3))->___Item2_1))->___collider_4), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&((&(((&__this->___enumerator_6))->____current_3))->___Item2_1))->___colliders_5), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&((&(((&__this->___enumerator_6))->____current_3))->___Item3_2))->___rigidbody_2), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&((&(((&__this->___enumerator_6))->____current_3))->___Item3_2))->___collider_4), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&((&(((&__this->___enumerator_6))->____current_3))->___Item3_2))->___colliders_5), (void*)NULL);
		#endif
		((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this)->___state_1 = 2;
		goto IL_0061;
	}

IL_002b:
	{
		Enumerator_t3ACCE1C7605054D2EF26F63B987FBD7CEA380901* L_5 = (Enumerator_t3ACCE1C7605054D2EF26F63B987FBD7CEA380901*)(&__this->___enumerator_6);
		ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 L_6;
		L_6 = Enumerator_get_Current_mE43C1A4AD61C4A5D58C7E5F6D98CD7DE40A64987_inline(L_5, il2cpp_rgctx_method(method->klass->rgctx_data, 10));
		V_1 = L_6;
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_7 = (Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*)__this->___predicate_4;
		if (!L_7)
		{
			goto IL_004d;
		}
	}
	{
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_8 = (Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*)__this->___predicate_4;
		ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 L_9 = V_1;
		NullCheck(L_8);
		bool L_10;
		L_10 = Func_2_Invoke_m12B4F5C7D4C30A85BD18305B0CDE8DF42312F402_inline(L_8, L_9, il2cpp_rgctx_method(method->klass->rgctx_data, 12));
		if (!L_10)
		{
			goto IL_0061;
		}
	}

IL_004d:
	{
		Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287* L_11 = (Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287*)__this->___selector_5;
		ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 L_12 = V_1;
		NullCheck(L_11);
		RuntimeObject* L_13;
		L_13 = Func_2_Invoke_m9EB50B06293708FB57A5A30A460483868FFF99D0_inline(L_11, L_12, il2cpp_rgctx_method(method->klass->rgctx_data, 13));
		((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this)->___current_2 = L_13;
		Il2CppCodeGenWriteBarrier((void**)(&((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this)->___current_2), (void*)L_13);
		return (bool)1;
	}

IL_0061:
	{
		Enumerator_t3ACCE1C7605054D2EF26F63B987FBD7CEA380901* L_14 = (Enumerator_t3ACCE1C7605054D2EF26F63B987FBD7CEA380901*)(&__this->___enumerator_6);
		bool L_15;
		L_15 = Enumerator_MoveNext_mD0B6A0F10BA08A58F39285C8C8309111A79A3F7A(L_14, il2cpp_rgctx_method(method->klass->rgctx_data, 15));
		if (L_15)
		{
			goto IL_002b;
		}
	}
	{
		NullCheck((Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this);
		VirtualActionInvoker0::Invoke(12 /* System.Void System.Linq.Enumerable/Iterator`1<System.Object>::Dispose() */, (Iterator_1_t99A1802EE86A3D5BF71B2DDB37F159C4AFA448EA*)__this);
	}

IL_0074:
	{
		return (bool)0;
	}
}
// System.Collections.Generic.IEnumerable`1<TResult> System.Linq.Enumerable/WhereSelectListIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Object>::Where(System.Func`2<TResult,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* WhereSelectListIterator_2_Where_mE279814C3B0287ED08BB8CC43AB0D2182E1CCED1_gshared (WhereSelectListIterator_2_tEB04EFC71ED85DC1E14F1FC388A8CCAF2FB25E8B* __this, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate0, const RuntimeMethod* method) 
{
	{
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_0 = ___predicate0;
		WhereEnumerableIterator_1_t1E787D13759F5A31C94B3FAED181402B25C278F4* L_1 = (WhereEnumerableIterator_1_t1E787D13759F5A31C94B3FAED181402B25C278F4*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 18));
		NullCheck(L_1);
		WhereEnumerableIterator_1__ctor_m8C0DA4CDA5431C03561F67C4393BB18CDD891F01(L_1, (RuntimeObject*)__this, L_0, il2cpp_rgctx_method(method->klass->rgctx_data, 19));
		return (RuntimeObject*)L_1;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void System.Linq.Enumerable/WhereSelectListIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Single>::.ctor(System.Collections.Generic.List`1<TSource>,System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectListIterator_2__ctor_m0889BA82694536AAAE4B2C793DC8825399744C3D_gshared (WhereSelectListIterator_2_t87543D52D098CB5DBA7AF3A904A5A1183466C3C6* __this, List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE* ___source0, Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* ___predicate1, Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72* ___selector2, const RuntimeMethod* method) 
{
	{
		Iterator_1__ctor_m7FEBAE985CDE5DD1CB255EB4A3D65F05AA8D2AEF((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 0));
		List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE* L_0 = ___source0;
		__this->___source_3 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___source_3), (void*)L_0);
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_1 = ___predicate1;
		__this->___predicate_4 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___predicate_4), (void*)L_1);
		Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72* L_2 = ___selector2;
		__this->___selector_5 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___selector_5), (void*)L_2);
		return;
	}
}
// System.Linq.Enumerable/Iterator`1<TResult> System.Linq.Enumerable/WhereSelectListIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Single>::Clone()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E* WhereSelectListIterator_2_Clone_m67A5B84A4E7E5E58A39A0E6A6433A51A6A2E24E9_gshared (WhereSelectListIterator_2_t87543D52D098CB5DBA7AF3A904A5A1183466C3C6* __this, const RuntimeMethod* method) 
{
	{
		List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE* L_0 = (List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE*)__this->___source_3;
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_1 = (Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*)__this->___predicate_4;
		Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72* L_2 = (Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72*)__this->___selector_5;
		WhereSelectListIterator_2_t87543D52D098CB5DBA7AF3A904A5A1183466C3C6* L_3 = (WhereSelectListIterator_2_t87543D52D098CB5DBA7AF3A904A5A1183466C3C6*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 3));
		NullCheck(L_3);
		WhereSelectListIterator_2__ctor_m0889BA82694536AAAE4B2C793DC8825399744C3D(L_3, L_0, L_1, L_2, il2cpp_rgctx_method(method->klass->rgctx_data, 6));
		return (Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)L_3;
	}
}
// System.Boolean System.Linq.Enumerable/WhereSelectListIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Single>::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool WhereSelectListIterator_2_MoveNext_m8EF15F0D64A667AB437BEBA20C3F757878BEDBA2_gshared (WhereSelectListIterator_2_t87543D52D098CB5DBA7AF3A904A5A1183466C3C6* __this, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		int32_t L_0 = (int32_t)((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this)->___state_1;
		V_0 = L_0;
		int32_t L_1 = V_0;
		if ((((int32_t)L_1) == ((int32_t)1)))
		{
			goto IL_0011;
		}
	}
	{
		int32_t L_2 = V_0;
		if ((((int32_t)L_2) == ((int32_t)2)))
		{
			goto IL_0061;
		}
	}
	{
		goto IL_0074;
	}

IL_0011:
	{
		List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE* L_3 = (List_1_t9C90AAE84BC8D7A985F7CFCFB4B8FC6722FF4DCE*)__this->___source_3;
		NullCheck(L_3);
		Enumerator_t3ACCE1C7605054D2EF26F63B987FBD7CEA380901 L_4;
		L_4 = List_1_GetEnumerator_m180F98F72F5DA91BF8696E010117148062025D82(L_3, il2cpp_rgctx_method(method->klass->rgctx_data, 8));
		__this->___enumerator_6 = L_4;
		Il2CppCodeGenWriteBarrier((void**)&(((&__this->___enumerator_6))->____list_0), (void*)NULL);
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&(((&__this->___enumerator_6))->____current_3))->___Item1_0), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&((&(((&__this->___enumerator_6))->____current_3))->___Item2_1))->___rigidbody_2), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&((&(((&__this->___enumerator_6))->____current_3))->___Item2_1))->___collider_4), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&((&(((&__this->___enumerator_6))->____current_3))->___Item2_1))->___colliders_5), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&((&(((&__this->___enumerator_6))->____current_3))->___Item3_2))->___rigidbody_2), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&((&(((&__this->___enumerator_6))->____current_3))->___Item3_2))->___collider_4), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((&((&(((&__this->___enumerator_6))->____current_3))->___Item3_2))->___colliders_5), (void*)NULL);
		#endif
		((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this)->___state_1 = 2;
		goto IL_0061;
	}

IL_002b:
	{
		Enumerator_t3ACCE1C7605054D2EF26F63B987FBD7CEA380901* L_5 = (Enumerator_t3ACCE1C7605054D2EF26F63B987FBD7CEA380901*)(&__this->___enumerator_6);
		ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 L_6;
		L_6 = Enumerator_get_Current_mE43C1A4AD61C4A5D58C7E5F6D98CD7DE40A64987_inline(L_5, il2cpp_rgctx_method(method->klass->rgctx_data, 10));
		V_1 = L_6;
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_7 = (Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*)__this->___predicate_4;
		if (!L_7)
		{
			goto IL_004d;
		}
	}
	{
		Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* L_8 = (Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12*)__this->___predicate_4;
		ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 L_9 = V_1;
		NullCheck(L_8);
		bool L_10;
		L_10 = Func_2_Invoke_m12B4F5C7D4C30A85BD18305B0CDE8DF42312F402_inline(L_8, L_9, il2cpp_rgctx_method(method->klass->rgctx_data, 12));
		if (!L_10)
		{
			goto IL_0061;
		}
	}

IL_004d:
	{
		Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72* L_11 = (Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72*)__this->___selector_5;
		ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 L_12 = V_1;
		NullCheck(L_11);
		float L_13;
		L_13 = Func_2_Invoke_mC888CBA13A68639AC714989A4F3DC91C432BB307_inline(L_11, L_12, il2cpp_rgctx_method(method->klass->rgctx_data, 13));
		((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this)->___current_2 = L_13;
		return (bool)1;
	}

IL_0061:
	{
		Enumerator_t3ACCE1C7605054D2EF26F63B987FBD7CEA380901* L_14 = (Enumerator_t3ACCE1C7605054D2EF26F63B987FBD7CEA380901*)(&__this->___enumerator_6);
		bool L_15;
		L_15 = Enumerator_MoveNext_mD0B6A0F10BA08A58F39285C8C8309111A79A3F7A(L_14, il2cpp_rgctx_method(method->klass->rgctx_data, 15));
		if (L_15)
		{
			goto IL_002b;
		}
	}
	{
		NullCheck((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this);
		VirtualActionInvoker0::Invoke(12 /* System.Void System.Linq.Enumerable/Iterator`1<System.Single>::Dispose() */, (Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this);
	}

IL_0074:
	{
		return (bool)0;
	}
}
// System.Collections.Generic.IEnumerable`1<TResult> System.Linq.Enumerable/WhereSelectListIterator`2<System.ValueTuple`3<System.Object,RBPhys.RBTrajectory,RBPhys.RBTrajectory>,System.Single>::Where(System.Func`2<TResult,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* WhereSelectListIterator_2_Where_m3691ED41A4BA4F7C09AB34760C2225BD74A564EA_gshared (WhereSelectListIterator_2_t87543D52D098CB5DBA7AF3A904A5A1183466C3C6* __this, Func_2_t49E998685259ADE759F9329BF66F20DE8667006E* ___predicate0, const RuntimeMethod* method) 
{
	{
		Func_2_t49E998685259ADE759F9329BF66F20DE8667006E* L_0 = ___predicate0;
		WhereEnumerableIterator_1_t7BB2D1D9F8A6E52243A87F45DFEEA4209D685F7C* L_1 = (WhereEnumerableIterator_1_t7BB2D1D9F8A6E52243A87F45DFEEA4209D685F7C*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 18));
		NullCheck(L_1);
		WhereEnumerableIterator_1__ctor_mCB49A03958EF827EF6CE47402259941EAB31D984(L_1, (RuntimeObject*)__this, L_0, il2cpp_rgctx_method(method->klass->rgctx_data, 19));
		return (RuntimeObject*)L_1;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void System.Linq.Enumerable/WhereSelectListIterator`2<System.Object,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::.ctor(System.Collections.Generic.List`1<TSource>,System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectListIterator_2__ctor_m6F364DC8CF50F39787083DBD5334AC40433CC8B8_gshared (WhereSelectListIterator_2_t6D877164A02A148F4B5F654B133D07C23FA7904A* __this, List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* ___source0, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate1, Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9* ___selector2, const RuntimeMethod* method) 
{
	{
		Iterator_1__ctor_m27CEA132FC2E7D7BA6A8529CE0647F2709FC29A3((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 0));
		List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* L_0 = ___source0;
		__this->___source_3 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___source_3), (void*)L_0);
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_1 = ___predicate1;
		__this->___predicate_4 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___predicate_4), (void*)L_1);
		Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9* L_2 = ___selector2;
		__this->___selector_5 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___selector_5), (void*)L_2);
		return;
	}
}
// System.Linq.Enumerable/Iterator`1<TResult> System.Linq.Enumerable/WhereSelectListIterator`2<System.Object,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::Clone()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40* WhereSelectListIterator_2_Clone_m47A8D689A5325B34C2209E57A623C0808DD9C40D_gshared (WhereSelectListIterator_2_t6D877164A02A148F4B5F654B133D07C23FA7904A* __this, const RuntimeMethod* method) 
{
	{
		List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* L_0 = (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D*)__this->___source_3;
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_1 = (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*)__this->___predicate_4;
		Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9* L_2 = (Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9*)__this->___selector_5;
		WhereSelectListIterator_2_t6D877164A02A148F4B5F654B133D07C23FA7904A* L_3 = (WhereSelectListIterator_2_t6D877164A02A148F4B5F654B133D07C23FA7904A*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 3));
		NullCheck(L_3);
		WhereSelectListIterator_2__ctor_m6F364DC8CF50F39787083DBD5334AC40433CC8B8(L_3, L_0, L_1, L_2, il2cpp_rgctx_method(method->klass->rgctx_data, 6));
		return (Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)L_3;
	}
}
// System.Boolean System.Linq.Enumerable/WhereSelectListIterator`2<System.Object,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool WhereSelectListIterator_2_MoveNext_m938B0336305650E221C15E3299430B3287EFC56D_gshared (WhereSelectListIterator_2_t6D877164A02A148F4B5F654B133D07C23FA7904A* __this, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	RuntimeObject* V_1 = NULL;
	{
		int32_t L_0 = (int32_t)((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this)->___state_1;
		V_0 = L_0;
		int32_t L_1 = V_0;
		if ((((int32_t)L_1) == ((int32_t)1)))
		{
			goto IL_0011;
		}
	}
	{
		int32_t L_2 = V_0;
		if ((((int32_t)L_2) == ((int32_t)2)))
		{
			goto IL_0061;
		}
	}
	{
		goto IL_0074;
	}

IL_0011:
	{
		List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* L_3 = (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D*)__this->___source_3;
		NullCheck(L_3);
		Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A L_4;
		L_4 = List_1_GetEnumerator_mD8294A7FA2BEB1929487127D476F8EC1CDC23BFC(L_3, il2cpp_rgctx_method(method->klass->rgctx_data, 8));
		__this->___enumerator_6 = L_4;
		Il2CppCodeGenWriteBarrier((void**)&(((&__this->___enumerator_6))->____list_0), (void*)NULL);
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&(((&__this->___enumerator_6))->____current_3), (void*)NULL);
		#endif
		((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this)->___state_1 = 2;
		goto IL_0061;
	}

IL_002b:
	{
		Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A* L_5 = (Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A*)(&__this->___enumerator_6);
		RuntimeObject* L_6;
		L_6 = Enumerator_get_Current_m6330F15D18EE4F547C05DF9BF83C5EB710376027_inline(L_5, il2cpp_rgctx_method(method->klass->rgctx_data, 10));
		V_1 = L_6;
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_7 = (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*)__this->___predicate_4;
		if (!L_7)
		{
			goto IL_004d;
		}
	}
	{
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_8 = (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*)__this->___predicate_4;
		RuntimeObject* L_9 = V_1;
		NullCheck(L_8);
		bool L_10;
		L_10 = Func_2_Invoke_m2014423FB900F135C8FF994125604FF9E6AAE829_inline(L_8, L_9, il2cpp_rgctx_method(method->klass->rgctx_data, 12));
		if (!L_10)
		{
			goto IL_0061;
		}
	}

IL_004d:
	{
		Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9* L_11 = (Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9*)__this->___selector_5;
		RuntimeObject* L_12 = V_1;
		NullCheck(L_11);
		ValueTuple_2_t761B36235D5F97F497A1CBCD077DF3858814FADE L_13;
		L_13 = Func_2_Invoke_m30EE337C21F1CA674046A64A8A2DA68A02701634_inline(L_11, L_12, il2cpp_rgctx_method(method->klass->rgctx_data, 13));
		((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this)->___current_2 = L_13;
		Il2CppCodeGenWriteBarrier((void**)&(((&((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this)->___current_2))->___Item1_0), (void*)NULL);
		return (bool)1;
	}

IL_0061:
	{
		Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A* L_14 = (Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A*)(&__this->___enumerator_6);
		bool L_15;
		L_15 = Enumerator_MoveNext_mE921CC8F29FBBDE7CC3209A0ED0D921D58D00BCB(L_14, il2cpp_rgctx_method(method->klass->rgctx_data, 15));
		if (L_15)
		{
			goto IL_002b;
		}
	}
	{
		NullCheck((Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this);
		VirtualActionInvoker0::Invoke(12 /* System.Void System.Linq.Enumerable/Iterator`1<System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::Dispose() */, (Iterator_1_tE7297E5C2863BB8F60B740D02A5747AA193D5D40*)__this);
	}

IL_0074:
	{
		return (bool)0;
	}
}
// System.Collections.Generic.IEnumerable`1<TResult> System.Linq.Enumerable/WhereSelectListIterator`2<System.Object,System.ValueTuple`2<System.Object,RBPhys.RBColliderAABB>>::Where(System.Func`2<TResult,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* WhereSelectListIterator_2_Where_m3C064A8F4B3270E6433C83AD0E6591B7EFA3F0C3_gshared (WhereSelectListIterator_2_t6D877164A02A148F4B5F654B133D07C23FA7904A* __this, Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B* ___predicate0, const RuntimeMethod* method) 
{
	{
		Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B* L_0 = ___predicate0;
		WhereEnumerableIterator_1_tB576B4EE76F70B81DDF11FA35260EC589825E7C5* L_1 = (WhereEnumerableIterator_1_tB576B4EE76F70B81DDF11FA35260EC589825E7C5*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 18));
		NullCheck(L_1);
		WhereEnumerableIterator_1__ctor_mC87BD639674AE32961C0AC5A5878F4144E7DA780(L_1, (RuntimeObject*)__this, L_0, il2cpp_rgctx_method(method->klass->rgctx_data, 19));
		return (RuntimeObject*)L_1;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void System.Linq.Enumerable/WhereSelectListIterator`2<System.Object,System.Single>::.ctor(System.Collections.Generic.List`1<TSource>,System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectListIterator_2__ctor_m22BAFF678F9378729DD9E5551BBA6BA69FDEEF94_gshared (WhereSelectListIterator_2_tFE32164179EE37B666D799CFB7F5167FB4C464D9* __this, List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* ___source0, Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* ___predicate1, Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12* ___selector2, const RuntimeMethod* method) 
{
	{
		Iterator_1__ctor_m7FEBAE985CDE5DD1CB255EB4A3D65F05AA8D2AEF((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this, il2cpp_rgctx_method(method->klass->rgctx_data, 0));
		List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* L_0 = ___source0;
		__this->___source_3 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___source_3), (void*)L_0);
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_1 = ___predicate1;
		__this->___predicate_4 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___predicate_4), (void*)L_1);
		Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12* L_2 = ___selector2;
		__this->___selector_5 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___selector_5), (void*)L_2);
		return;
	}
}
// System.Linq.Enumerable/Iterator`1<TResult> System.Linq.Enumerable/WhereSelectListIterator`2<System.Object,System.Single>::Clone()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E* WhereSelectListIterator_2_Clone_m29FF529DD718DD7EEFEE0E1128661AD8EF2C534E_gshared (WhereSelectListIterator_2_tFE32164179EE37B666D799CFB7F5167FB4C464D9* __this, const RuntimeMethod* method) 
{
	{
		List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* L_0 = (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D*)__this->___source_3;
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_1 = (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*)__this->___predicate_4;
		Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12* L_2 = (Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12*)__this->___selector_5;
		WhereSelectListIterator_2_tFE32164179EE37B666D799CFB7F5167FB4C464D9* L_3 = (WhereSelectListIterator_2_tFE32164179EE37B666D799CFB7F5167FB4C464D9*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 3));
		NullCheck(L_3);
		WhereSelectListIterator_2__ctor_m22BAFF678F9378729DD9E5551BBA6BA69FDEEF94(L_3, L_0, L_1, L_2, il2cpp_rgctx_method(method->klass->rgctx_data, 6));
		return (Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)L_3;
	}
}
// System.Boolean System.Linq.Enumerable/WhereSelectListIterator`2<System.Object,System.Single>::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool WhereSelectListIterator_2_MoveNext_m0B0C765BFD4BA1167D123810AF36952282481BA6_gshared (WhereSelectListIterator_2_tFE32164179EE37B666D799CFB7F5167FB4C464D9* __this, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	RuntimeObject* V_1 = NULL;
	{
		int32_t L_0 = (int32_t)((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this)->___state_1;
		V_0 = L_0;
		int32_t L_1 = V_0;
		if ((((int32_t)L_1) == ((int32_t)1)))
		{
			goto IL_0011;
		}
	}
	{
		int32_t L_2 = V_0;
		if ((((int32_t)L_2) == ((int32_t)2)))
		{
			goto IL_0061;
		}
	}
	{
		goto IL_0074;
	}

IL_0011:
	{
		List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* L_3 = (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D*)__this->___source_3;
		NullCheck(L_3);
		Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A L_4;
		L_4 = List_1_GetEnumerator_mD8294A7FA2BEB1929487127D476F8EC1CDC23BFC(L_3, il2cpp_rgctx_method(method->klass->rgctx_data, 8));
		__this->___enumerator_6 = L_4;
		Il2CppCodeGenWriteBarrier((void**)&(((&__this->___enumerator_6))->____list_0), (void*)NULL);
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&(((&__this->___enumerator_6))->____current_3), (void*)NULL);
		#endif
		((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this)->___state_1 = 2;
		goto IL_0061;
	}

IL_002b:
	{
		Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A* L_5 = (Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A*)(&__this->___enumerator_6);
		RuntimeObject* L_6;
		L_6 = Enumerator_get_Current_m6330F15D18EE4F547C05DF9BF83C5EB710376027_inline(L_5, il2cpp_rgctx_method(method->klass->rgctx_data, 10));
		V_1 = L_6;
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_7 = (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*)__this->___predicate_4;
		if (!L_7)
		{
			goto IL_004d;
		}
	}
	{
		Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* L_8 = (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00*)__this->___predicate_4;
		RuntimeObject* L_9 = V_1;
		NullCheck(L_8);
		bool L_10;
		L_10 = Func_2_Invoke_m2014423FB900F135C8FF994125604FF9E6AAE829_inline(L_8, L_9, il2cpp_rgctx_method(method->klass->rgctx_data, 12));
		if (!L_10)
		{
			goto IL_0061;
		}
	}

IL_004d:
	{
		Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12* L_11 = (Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12*)__this->___selector_5;
		RuntimeObject* L_12 = V_1;
		NullCheck(L_11);
		float L_13;
		L_13 = Func_2_Invoke_mBE16A5FDA5E80CCBA51D69334EF21C0F03D353AF_inline(L_11, L_12, il2cpp_rgctx_method(method->klass->rgctx_data, 13));
		((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this)->___current_2 = L_13;
		return (bool)1;
	}

IL_0061:
	{
		Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A* L_14 = (Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A*)(&__this->___enumerator_6);
		bool L_15;
		L_15 = Enumerator_MoveNext_mE921CC8F29FBBDE7CC3209A0ED0D921D58D00BCB(L_14, il2cpp_rgctx_method(method->klass->rgctx_data, 15));
		if (L_15)
		{
			goto IL_002b;
		}
	}
	{
		NullCheck((Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this);
		VirtualActionInvoker0::Invoke(12 /* System.Void System.Linq.Enumerable/Iterator`1<System.Single>::Dispose() */, (Iterator_1_t86D8BDE63EBFB97A0B7A8464AC2992725EE2281E*)__this);
	}

IL_0074:
	{
		return (bool)0;
	}
}
// System.Collections.Generic.IEnumerable`1<TResult> System.Linq.Enumerable/WhereSelectListIterator`2<System.Object,System.Single>::Where(System.Func`2<TResult,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* WhereSelectListIterator_2_Where_m4A3DB49D03860A66FF85F000DEB2E2D8688DAC49_gshared (WhereSelectListIterator_2_tFE32164179EE37B666D799CFB7F5167FB4C464D9* __this, Func_2_t49E998685259ADE759F9329BF66F20DE8667006E* ___predicate0, const RuntimeMethod* method) 
{
	{
		Func_2_t49E998685259ADE759F9329BF66F20DE8667006E* L_0 = ___predicate0;
		WhereEnumerableIterator_1_t7BB2D1D9F8A6E52243A87F45DFEEA4209D685F7C* L_1 = (WhereEnumerableIterator_1_t7BB2D1D9F8A6E52243A87F45DFEEA4209D685F7C*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 18));
		NullCheck(L_1);
		WhereEnumerableIterator_1__ctor_mCB49A03958EF827EF6CE47402259941EAB31D984(L_1, (RuntimeObject*)__this, L_0, il2cpp_rgctx_method(method->klass->rgctx_data, 19));
		return (RuntimeObject*)L_1;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void System.Linq.Enumerable/WhereSelectListIterator`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::.ctor(System.Collections.Generic.List`1<TSource>,System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WhereSelectListIterator_2__ctor_m6BFCBB5460270ED1896D24DC7E3B83F4950D2140_gshared (/*System.Linq.Enumerable/WhereSelectListIterator`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>*/WhereSelectListIterator_2_t86EE6817E8A1706688C6D82D82C9D44BC99CC336* __this, List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* ___source0, Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* ___predicate1, Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0* ___selector2, const RuntimeMethod* method) 
{
	{
		InvokerActionInvoker0::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 0)), il2cpp_rgctx_method(method->klass->rgctx_data, 0), (Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0*)__this);
		List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* L_0 = ___source0;
		il2cpp_codegen_write_instance_field_data<List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),0), L_0);
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_1 = ___predicate1;
		il2cpp_codegen_write_instance_field_data<Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B*>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),1), L_1);
		Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0* L_2 = ___selector2;
		il2cpp_codegen_write_instance_field_data<Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0*>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),2), L_2);
		return;
	}
}
// System.Linq.Enumerable/Iterator`1<TResult> System.Linq.Enumerable/WhereSelectListIterator`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::Clone()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0* WhereSelectListIterator_2_Clone_m8EC8E684FFDC3BC579DF37C08993B7F80966639D_gshared (/*System.Linq.Enumerable/WhereSelectListIterator`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>*/WhereSelectListIterator_2_t86EE6817E8A1706688C6D82D82C9D44BC99CC336* __this, const RuntimeMethod* method) 
{
	{
		List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* L_0 = *(List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),0));
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_1 = *(Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),1));
		Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0* L_2 = *(Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),2));
		WhereSelectListIterator_2_t86EE6817E8A1706688C6D82D82C9D44BC99CC336* L_3 = (WhereSelectListIterator_2_t86EE6817E8A1706688C6D82D82C9D44BC99CC336*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 3));
		NullCheck(L_3);
		InvokerActionInvoker3< List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B*, Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 6)), il2cpp_rgctx_method(method->klass->rgctx_data, 6), L_3, L_0, L_1, L_2);
		return (Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0*)L_3;
	}
}
// System.Boolean System.Linq.Enumerable/WhereSelectListIterator`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::MoveNext()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool WhereSelectListIterator_2_MoveNext_mBB81EEF5DFFEBDDB1AC24116FAD1D13505525569_gshared (/*System.Linq.Enumerable/WhereSelectListIterator`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>*/WhereSelectListIterator_2_t86EE6817E8A1706688C6D82D82C9D44BC99CC336* __this, const RuntimeMethod* method) 
{
	// sizeof(TSource)
	const uint32_t SizeOf_TSource_tEB7490DB2885922B8C60E28873F5DB811BD9CEB3 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->klass->rgctx_data, 11));
	// sizeof(System.Collections.Generic.List`1/Enumerator<TSource>)
	const uint32_t SizeOf_Enumerator_t8A622325AF1352D3AB0ECDBB45A0AFB7AF959716 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->klass->rgctx_data, 9));
	// sizeof(TResult)
	const uint32_t SizeOf_TResult_t11AC9139084FDCB528CAF75FE5166467D3329A05 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->klass->rgctx_data, 14));
	// TSource
	const Il2CppFullySharedGenericAny L_5 = alloca(SizeOf_TSource_tEB7490DB2885922B8C60E28873F5DB811BD9CEB3);
	const Il2CppFullySharedGenericAny L_8 = L_5;
	const Il2CppFullySharedGenericAny L_11 = L_5;
	// TResult
	const Il2CppFullySharedGenericAny L_12 = alloca(SizeOf_TResult_t11AC9139084FDCB528CAF75FE5166467D3329A05);
	// System.Collections.Generic.List`1/Enumerator<TSource>
	const Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF L_4 = alloca(SizeOf_Enumerator_t8A622325AF1352D3AB0ECDBB45A0AFB7AF959716);
	int32_t V_0 = 0;
	Il2CppFullySharedGenericAny V_1 = alloca(SizeOf_TSource_tEB7490DB2885922B8C60E28873F5DB811BD9CEB3);
	memset(V_1, 0, SizeOf_TSource_tEB7490DB2885922B8C60E28873F5DB811BD9CEB3);
	{
		int32_t L_0 = *(int32_t*)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 7),1));
		V_0 = L_0;
		int32_t L_1 = V_0;
		if ((((int32_t)L_1) == ((int32_t)1)))
		{
			goto IL_0011;
		}
	}
	{
		int32_t L_2 = V_0;
		if ((((int32_t)L_2) == ((int32_t)2)))
		{
			goto IL_0061;
		}
	}
	{
		goto IL_0074;
	}

IL_0011:
	{
		List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* L_3 = *(List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),0));
		NullCheck(L_3);
		InvokerActionInvoker1< Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 8)), il2cpp_rgctx_method(method->klass->rgctx_data, 8), L_3, (Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF*)L_4);
		il2cpp_codegen_write_instance_field_data(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),3), L_4, SizeOf_Enumerator_t8A622325AF1352D3AB0ECDBB45A0AFB7AF959716);
		il2cpp_codegen_write_instance_field_data<int32_t>(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 7),1), 2);
		goto IL_0061;
	}

IL_002b:
	{
		InvokerActionInvoker1< Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 10)), il2cpp_rgctx_method(method->klass->rgctx_data, 10), (((Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF*)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),3)))), (Il2CppFullySharedGenericAny*)L_5);
		il2cpp_codegen_memcpy(V_1, L_5, SizeOf_TSource_tEB7490DB2885922B8C60E28873F5DB811BD9CEB3);
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_6 = *(Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),1));
		if (!L_6)
		{
			goto IL_004d;
		}
	}
	{
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_7 = *(Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),1));
		il2cpp_codegen_memcpy(L_8, V_1, SizeOf_TSource_tEB7490DB2885922B8C60E28873F5DB811BD9CEB3);
		NullCheck(L_7);
		bool L_9;
		L_9 = InvokerFuncInvoker1< bool, Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 12)), il2cpp_rgctx_method(method->klass->rgctx_data, 12), L_7, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->klass->rgctx_data, 11)) ? L_8: *(void**)L_8));
		if (!L_9)
		{
			goto IL_0061;
		}
	}

IL_004d:
	{
		Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0* L_10 = *(Func_2_t7F5F5324CE2DDB7001B68FFE29A5D9F907139FB0**)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),2));
		il2cpp_codegen_memcpy(L_11, V_1, SizeOf_TSource_tEB7490DB2885922B8C60E28873F5DB811BD9CEB3);
		NullCheck(L_10);
		InvokerActionInvoker2< Il2CppFullySharedGenericAny, Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 13)), il2cpp_rgctx_method(method->klass->rgctx_data, 13), L_10, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->klass->rgctx_data, 11)) ? L_11: *(void**)L_11), (Il2CppFullySharedGenericAny*)L_12);
		il2cpp_codegen_write_instance_field_data(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 7),2), L_12, SizeOf_TResult_t11AC9139084FDCB528CAF75FE5166467D3329A05);
		return (bool)1;
	}

IL_0061:
	{
		bool L_13;
		L_13 = InvokerFuncInvoker0< bool >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 15)), il2cpp_rgctx_method(method->klass->rgctx_data, 15), (((Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF*)il2cpp_codegen_get_instance_field_data_pointer(__this, il2cpp_rgctx_field(il2cpp_rgctx_data(method->klass->rgctx_data, 3),3)))));
		if (L_13)
		{
			goto IL_002b;
		}
	}
	{
		NullCheck((Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0*)__this);
		VirtualActionInvoker0Invoker::Invoke(12 /* System.Void System.Linq.Enumerable/Iterator`1<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::Dispose() */, (Iterator_1_t0F1D8198E840368AC82131EC1FF03EB76BCE73B0*)__this);
	}

IL_0074:
	{
		return (bool)0;
	}
}
// System.Collections.Generic.IEnumerable`1<TResult> System.Linq.Enumerable/WhereSelectListIterator`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>::Where(System.Func`2<TResult,System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* WhereSelectListIterator_2_Where_m1739BDD134D3AF5A55DBB06AEE130B0C58E47014_gshared (/*System.Linq.Enumerable/WhereSelectListIterator`2<Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType,Unity.IL2CPP.Metadata.__Il2CppFullySharedGenericType>*/WhereSelectListIterator_2_t86EE6817E8A1706688C6D82D82C9D44BC99CC336* __this, Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* ___predicate0, const RuntimeMethod* method) 
{
	{
		Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* L_0 = ___predicate0;
		WhereEnumerableIterator_1_t8B24528558F527941435C4FE1D046216FE4F277B* L_1 = (WhereEnumerableIterator_1_t8B24528558F527941435C4FE1D046216FE4F277B*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->klass->rgctx_data, 18));
		NullCheck(L_1);
		InvokerActionInvoker2< RuntimeObject*, Func_2_t19E50C11C3E1F20B5A8FDB85D7DD353B6DFF868B* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 19)), il2cpp_rgctx_method(method->klass->rgctx_data, 19), L_1, (RuntimeObject*)__this, L_0);
		return (RuntimeObject*)L_1;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void DebuggerSupport_AddToActiveTasks_mF592C309CB2AE9C85563BE114DDDB6D226AD9E3E_inline (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* ___task0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		il2cpp_codegen_runtime_class_init_inline(Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572_il2cpp_TypeInfo_var);
		bool L_0 = ((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572_StaticFields*)il2cpp_codegen_static_fields_for(Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572_il2cpp_TypeInfo_var))->___s_asyncDebuggingEnabled_9;
		if (!L_0)
		{
			goto IL_000d;
		}
	}
	{
		Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* L_1 = ___task0;
		il2cpp_codegen_runtime_class_init_inline(DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var);
		DebuggerSupport_AddToActiveTasksNonInlined_m28785ADBB0443B6CCDC12C4A7DA297F1FFAEF889(L_1, NULL);
	}

IL_000d:
	{
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void DebuggerSupport_RemoveFromActiveTasks_m19229D30DAA2447DDAB524F2A0E9718D070A1251_inline (Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* ___task0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		il2cpp_codegen_runtime_class_init_inline(Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572_il2cpp_TypeInfo_var);
		bool L_0 = ((Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572_StaticFields*)il2cpp_codegen_static_fields_for(Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572_il2cpp_TypeInfo_var))->___s_asyncDebuggingEnabled_9;
		if (!L_0)
		{
			goto IL_000d;
		}
	}
	{
		Task_t751C4CC3ECD055BABA8A0B6A5DFBB4283DCA8572* L_1 = ___task0;
		il2cpp_codegen_runtime_class_init_inline(DebuggerSupport_tDD9572640CC0FDE885CA0394A44CB639ADFF69E2_il2cpp_TypeInfo_var);
		DebuggerSupport_RemoveFromActiveTasksNonInlined_m328F6FE7F9066EAD92528B6AD6BB08B12BA9A2C0(L_1, NULL);
	}

IL_000d:
	{
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Func_2_Invoke_m2014423FB900F135C8FF994125604FF9E6AAE829_gshared_inline (Func_2_tE1F0D41563EE092E5E5540B061449FDE88F1DC00* __this, RuntimeObject* ___arg0, const RuntimeMethod* method) 
{
	typedef bool (*FunctionPointerType) (RuntimeObject*, RuntimeObject*, const RuntimeMethod*);
	return ((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, ___arg0, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Func_2_Invoke_m1DC9E7C418A082CE71CE5A7F12B8F8F30528804F_gshared_inline (Func_2_tC0964C5A0C02874F30240243CCBDF66294E8AE2B* __this, ValueTuple_2_t761B36235D5F97F497A1CBCD077DF3858814FADE ___arg0, const RuntimeMethod* method) 
{
	typedef bool (*FunctionPointerType) (RuntimeObject*, ValueTuple_2_t761B36235D5F97F497A1CBCD077DF3858814FADE, const RuntimeMethod*);
	return ((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, ___arg0, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Func_2_Invoke_m1FE6F2A4EC23CC595897C55AE7B0BDA8969044D7_gshared_inline (Func_2_t49E998685259ADE759F9329BF66F20DE8667006E* __this, float ___arg0, const RuntimeMethod* method) 
{
	typedef bool (*FunctionPointerType) (RuntimeObject*, float, const RuntimeMethod*);
	return ((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, ___arg0, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_get_Current_m6330F15D18EE4F547C05DF9BF83C5EB710376027_gshared_inline (Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A* __this, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = (RuntimeObject*)__this->____current_3;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Func_2_Invoke_m12B4F5C7D4C30A85BD18305B0CDE8DF42312F402_gshared_inline (Func_2_tA80B3567EDF529323D1DE2003E1133EE68634D12* __this, ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 ___arg0, const RuntimeMethod* method) 
{
	typedef bool (*FunctionPointerType) (RuntimeObject*, ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48, const RuntimeMethod*);
	return ((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, ___arg0, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ValueTuple_2_t761B36235D5F97F497A1CBCD077DF3858814FADE Func_2_Invoke_m3794F6EAED760CA17FC7A24439DC423AC6BF8FB9_gshared_inline (Func_2_tB1C222A0D0463B91F5C0F9A1941371A6D638F53B* __this, ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 ___arg0, const RuntimeMethod* method) 
{
	typedef ValueTuple_2_t761B36235D5F97F497A1CBCD077DF3858814FADE (*FunctionPointerType) (RuntimeObject*, ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48, const RuntimeMethod*);
	return ((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, ___arg0, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* Func_2_Invoke_m9EB50B06293708FB57A5A30A460483868FFF99D0_gshared_inline (Func_2_t51D8BA397DF605977B07DA37FC7E148968C99287* __this, ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 ___arg0, const RuntimeMethod* method) 
{
	typedef RuntimeObject* (*FunctionPointerType) (RuntimeObject*, ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48, const RuntimeMethod*);
	return ((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, ___arg0, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Func_2_Invoke_mC888CBA13A68639AC714989A4F3DC91C432BB307_gshared_inline (Func_2_tD4FE311BA22F0F39BADCC5955808B127E440ED72* __this, ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 ___arg0, const RuntimeMethod* method) 
{
	typedef float (*FunctionPointerType) (RuntimeObject*, ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48, const RuntimeMethod*);
	return ((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, ___arg0, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ValueTuple_2_t761B36235D5F97F497A1CBCD077DF3858814FADE Func_2_Invoke_m30EE337C21F1CA674046A64A8A2DA68A02701634_gshared_inline (Func_2_t18A572E254D1C33FA7A3D67C1841F6F1F2073BF9* __this, RuntimeObject* ___arg0, const RuntimeMethod* method) 
{
	typedef ValueTuple_2_t761B36235D5F97F497A1CBCD077DF3858814FADE (*FunctionPointerType) (RuntimeObject*, RuntimeObject*, const RuntimeMethod*);
	return ((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, ___arg0, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Func_2_Invoke_mBE16A5FDA5E80CCBA51D69334EF21C0F03D353AF_gshared_inline (Func_2_tB5C40A90702B6A6A2E315FD927EEFC9FB69F2B12* __this, RuntimeObject* ___arg0, const RuntimeMethod* method) 
{
	typedef float (*FunctionPointerType) (RuntimeObject*, RuntimeObject*, const RuntimeMethod*);
	return ((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, ___arg0, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 Enumerator_get_Current_mE43C1A4AD61C4A5D58C7E5F6D98CD7DE40A64987_gshared_inline (Enumerator_t3ACCE1C7605054D2EF26F63B987FBD7CEA380901* __this, const RuntimeMethod* method) 
{
	{
		ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48 L_0 = (ValueTuple_3_t506B2EF3A6252D5A253987E07637236E92E5DF48)__this->____current_3;
		return L_0;
	}
}
