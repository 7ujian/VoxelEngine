﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class Vox_Int3Wrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(Vox.Int3), null);
		L.RegFunction("Equals", Equals);
		L.RegFunction("GetHashCode", GetHashCode);
		L.RegFunction("ToString", ToString);
		L.RegFunction("New", _CreateVox_Int3);
		L.RegFunction("__add", op_Addition);
		L.RegFunction("__sub", op_Subtraction);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__unm", op_UnaryNegation);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("Zero", get_Zero, set_Zero);
		L.RegVar("Infinity", get_Infinity, set_Infinity);
		L.RegVar("x", get_x, set_x);
		L.RegVar("y", get_y, set_y);
		L.RegVar("z", get_z, set_z);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateVox_Int3(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				Vox.Int3 obj = new Vox.Int3();
				ToLua.PushValue(L, obj);
				return 1;
			}
			else if (count == 1)
			{
				int arg0 = (int)LuaDLL.luaL_checknumber(L, 1);
				Vox.Int3 obj = new Vox.Int3(arg0);
				ToLua.PushValue(L, obj);
				return 1;
			}
			else if (count == 2)
			{
				int arg0 = (int)LuaDLL.luaL_checknumber(L, 1);
				int arg1 = (int)LuaDLL.luaL_checknumber(L, 2);
				Vox.Int3 obj = new Vox.Int3(arg0, arg1);
				ToLua.PushValue(L, obj);
				return 1;
			}
			else if (count == 3)
			{
				int arg0 = (int)LuaDLL.luaL_checknumber(L, 1);
				int arg1 = (int)LuaDLL.luaL_checknumber(L, 2);
				int arg2 = (int)LuaDLL.luaL_checknumber(L, 3);
				Vox.Int3 obj = new Vox.Int3(arg0, arg1, arg2);
				ToLua.PushValue(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: Vox.Int3.New");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int op_Addition(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Vox.Int3 arg0 = StackTraits<Vox.Int3>.Check(L, 1);
			Vox.Int3 arg1 = StackTraits<Vox.Int3>.Check(L, 2);
			Vox.Int3 o = arg0 + arg1;
			ToLua.PushValue(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int op_Subtraction(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Vox.Int3 arg0 = StackTraits<Vox.Int3>.Check(L, 1);
			Vox.Int3 arg1 = StackTraits<Vox.Int3>.Check(L, 2);
			Vox.Int3 o = arg0 - arg1;
			ToLua.PushValue(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int op_UnaryNegation(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Vox.Int3 arg0 = StackTraits<Vox.Int3>.Check(L, 1);
			Vox.Int3 o = -arg0;
			ToLua.PushValue(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int op_Equality(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Vox.Int3 arg0 = StackTraits<Vox.Int3>.To(L, 1);
			Vox.Int3 arg1 = StackTraits<Vox.Int3>.To(L, 2);
			bool o = arg0 == arg1;
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Equals(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && TypeChecker.CheckTypes<Vox.Int3>(L, 2))
			{
				Vox.Int3 obj = (Vox.Int3)ToLua.CheckObject(L, 1, typeof(Vox.Int3));
				Vox.Int3 arg0 = StackTraits<Vox.Int3>.To(L, 2);
				bool o = obj.Equals(arg0);
				LuaDLL.lua_pushboolean(L, o);
				ToLua.SetBack(L, 1, obj);
				return 1;
			}
			else if (count == 2 && TypeChecker.CheckTypes<object>(L, 2))
			{
				Vox.Int3 obj = (Vox.Int3)ToLua.CheckObject(L, 1, typeof(Vox.Int3));
				object arg0 = ToLua.ToVarObject(L, 2);
				bool o = obj.Equals(arg0);
				LuaDLL.lua_pushboolean(L, o);
				ToLua.SetBack(L, 1, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: Vox.Int3.Equals");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetHashCode(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Vox.Int3 obj = (Vox.Int3)ToLua.CheckObject(L, 1, typeof(Vox.Int3));
			int o = obj.GetHashCode();
			LuaDLL.lua_pushinteger(L, o);
			ToLua.SetBack(L, 1, obj);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ToString(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Vox.Int3 obj = (Vox.Int3)ToLua.CheckObject(L, 1, typeof(Vox.Int3));
			string o = obj.ToString();
			LuaDLL.lua_pushstring(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Zero(IntPtr L)
	{
		try
		{
			ToLua.PushValue(L, Vox.Int3.Zero);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Infinity(IntPtr L)
	{
		try
		{
			ToLua.PushValue(L, Vox.Int3.Infinity);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_x(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			Vox.Int3 obj = (Vox.Int3)o;
			int ret = obj.x;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index x on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_y(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			Vox.Int3 obj = (Vox.Int3)o;
			int ret = obj.y;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index y on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_z(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			Vox.Int3 obj = (Vox.Int3)o;
			int ret = obj.z;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index z on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Zero(IntPtr L)
	{
		try
		{
			Vox.Int3 arg0 = StackTraits<Vox.Int3>.Check(L, 2);
			Vox.Int3.Zero = arg0;
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Infinity(IntPtr L)
	{
		try
		{
			Vox.Int3 arg0 = StackTraits<Vox.Int3>.Check(L, 2);
			Vox.Int3.Infinity = arg0;
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_x(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			Vox.Int3 obj = (Vox.Int3)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.x = arg0;
			ToLua.SetBack(L, 1, obj);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index x on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_y(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			Vox.Int3 obj = (Vox.Int3)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.y = arg0;
			ToLua.SetBack(L, 1, obj);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index y on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_z(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			Vox.Int3 obj = (Vox.Int3)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.z = arg0;
			ToLua.SetBack(L, 1, obj);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index z on a nil value");
		}
	}
}

