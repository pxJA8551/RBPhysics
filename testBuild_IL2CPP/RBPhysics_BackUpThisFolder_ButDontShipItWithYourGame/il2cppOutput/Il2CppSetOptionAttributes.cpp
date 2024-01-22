#include "pch-cpp.hpp"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include <limits>
#include <stdint.h>



// System.Attribute
struct Attribute_tFDA8EFEFB0711976D22474794576DAF28F7440AA;
// Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute
struct Il2CppSetOptionAttribute_t10C12A91C3755D0F39207F1AA85D95395123A22B;



IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// <Module>
struct U3CModuleU3E_t2DBC2FDFA1FE8D5833B1F0FD852624C758C90CC8 
{
};
struct Il2CppArrayBounds;

// System.Attribute
struct Attribute_tFDA8EFEFB0711976D22474794576DAF28F7440AA  : public RuntimeObject
{
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

// Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute
struct Il2CppSetOptionAttribute_t10C12A91C3755D0F39207F1AA85D95395123A22B  : public Attribute_tFDA8EFEFB0711976D22474794576DAF28F7440AA
{
	// Unity.IL2CPP.CompilerServices.Option Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute::<Option>k__BackingField
	int32_t ___U3COptionU3Ek__BackingField_0;
	// System.Object Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute::<Value>k__BackingField
	RuntimeObject* ___U3CValueU3Ek__BackingField_1;
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
#ifdef __clang__
#pragma clang diagnostic pop
#endif



// System.Void System.Attribute::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Attribute__ctor_m79ED1BF1EE36D1E417BA89A0D9F91F8AAD8D19E2 (Attribute_tFDA8EFEFB0711976D22474794576DAF28F7440AA* __this, const RuntimeMethod* method) ;
// System.Void Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute::set_Option(Unity.IL2CPP.CompilerServices.Option)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Il2CppSetOptionAttribute_set_Option_m44C29DDCAABF761AC4393237D4C484A963C4B1CA_inline (Il2CppSetOptionAttribute_t10C12A91C3755D0F39207F1AA85D95395123A22B* __this, int32_t ___value0, const RuntimeMethod* method) ;
// System.Void Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute::set_Value(System.Object)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Il2CppSetOptionAttribute_set_Value_m5D35EE09958D42351CBAC6F679A9BC11F4B351AE_inline (Il2CppSetOptionAttribute_t10C12A91C3755D0F39207F1AA85D95395123A22B* __this, RuntimeObject* ___value0, const RuntimeMethod* method) ;
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Unity.IL2CPP.CompilerServices.Option Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute::get_Option()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Il2CppSetOptionAttribute_get_Option_mF53180EB3F041281980A0AB24C2A47507A5EDF64 (Il2CppSetOptionAttribute_t10C12A91C3755D0F39207F1AA85D95395123A22B* __this, const RuntimeMethod* method) 
{
	{
		// public Option Option { get; private set; }
		int32_t L_0 = __this->___U3COptionU3Ek__BackingField_0;
		return L_0;
	}
}
// System.Void Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute::set_Option(Unity.IL2CPP.CompilerServices.Option)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Il2CppSetOptionAttribute_set_Option_m44C29DDCAABF761AC4393237D4C484A963C4B1CA (Il2CppSetOptionAttribute_t10C12A91C3755D0F39207F1AA85D95395123A22B* __this, int32_t ___value0, const RuntimeMethod* method) 
{
	{
		// public Option Option { get; private set; }
		int32_t L_0 = ___value0;
		__this->___U3COptionU3Ek__BackingField_0 = L_0;
		return;
	}
}
// System.Object Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute::get_Value()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Il2CppSetOptionAttribute_get_Value_m16300D148F4B1BF1A184E25083F920A68BF5078C (Il2CppSetOptionAttribute_t10C12A91C3755D0F39207F1AA85D95395123A22B* __this, const RuntimeMethod* method) 
{
	{
		// public object Value { get; private set; }
		RuntimeObject* L_0 = __this->___U3CValueU3Ek__BackingField_1;
		return L_0;
	}
}
// System.Void Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute::set_Value(System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Il2CppSetOptionAttribute_set_Value_m5D35EE09958D42351CBAC6F679A9BC11F4B351AE (Il2CppSetOptionAttribute_t10C12A91C3755D0F39207F1AA85D95395123A22B* __this, RuntimeObject* ___value0, const RuntimeMethod* method) 
{
	{
		// public object Value { get; private set; }
		RuntimeObject* L_0 = ___value0;
		__this->___U3CValueU3Ek__BackingField_1 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CValueU3Ek__BackingField_1), (void*)L_0);
		return;
	}
}
// System.Void Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute::.ctor(Unity.IL2CPP.CompilerServices.Option,System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Il2CppSetOptionAttribute__ctor_mB495C3F6550C855312DF244E37D181F8C60B41FE (Il2CppSetOptionAttribute_t10C12A91C3755D0F39207F1AA85D95395123A22B* __this, int32_t ___option0, RuntimeObject* ___value1, const RuntimeMethod* method) 
{
	{
		// public Il2CppSetOptionAttribute(Option option, object value)
		Attribute__ctor_m79ED1BF1EE36D1E417BA89A0D9F91F8AAD8D19E2(__this, NULL);
		// Option = option;
		int32_t L_0 = ___option0;
		Il2CppSetOptionAttribute_set_Option_m44C29DDCAABF761AC4393237D4C484A963C4B1CA_inline(__this, L_0, NULL);
		// Value = value;
		RuntimeObject* L_1 = ___value1;
		Il2CppSetOptionAttribute_set_Value_m5D35EE09958D42351CBAC6F679A9BC11F4B351AE_inline(__this, L_1, NULL);
		// }
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Il2CppSetOptionAttribute_set_Option_m44C29DDCAABF761AC4393237D4C484A963C4B1CA_inline (Il2CppSetOptionAttribute_t10C12A91C3755D0F39207F1AA85D95395123A22B* __this, int32_t ___value0, const RuntimeMethod* method) 
{
	{
		// public Option Option { get; private set; }
		int32_t L_0 = ___value0;
		__this->___U3COptionU3Ek__BackingField_0 = L_0;
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Il2CppSetOptionAttribute_set_Value_m5D35EE09958D42351CBAC6F679A9BC11F4B351AE_inline (Il2CppSetOptionAttribute_t10C12A91C3755D0F39207F1AA85D95395123A22B* __this, RuntimeObject* ___value0, const RuntimeMethod* method) 
{
	{
		// public object Value { get; private set; }
		RuntimeObject* L_0 = ___value0;
		__this->___U3CValueU3Ek__BackingField_1 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CValueU3Ek__BackingField_1), (void*)L_0);
		return;
	}
}
