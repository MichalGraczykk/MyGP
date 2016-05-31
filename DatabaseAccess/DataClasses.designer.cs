﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseAccess
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="MyGraduationProject.Database")]
	public partial class DataClassesDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertItem(Item instance);
    partial void UpdateItem(Item instance);
    partial void DeleteItem(Item instance);
    partial void InsertUsersAdress(UsersAdress instance);
    partial void UpdateUsersAdress(UsersAdress instance);
    partial void DeleteUsersAdress(UsersAdress instance);
    partial void InsertUser(User instance);
    partial void UpdateUser(User instance);
    partial void DeleteUser(User instance);
    partial void InsertState(State instance);
    partial void UpdateState(State instance);
    partial void DeleteState(State instance);
    partial void InsertRole(Role instance);
    partial void UpdateRole(Role instance);
    partial void DeleteRole(Role instance);
    partial void InsertReservationStatuse(ReservationStatuse instance);
    partial void UpdateReservationStatuse(ReservationStatuse instance);
    partial void DeleteReservationStatuse(ReservationStatuse instance);
    partial void InsertReservation(Reservation instance);
    partial void UpdateReservation(Reservation instance);
    partial void DeleteReservation(Reservation instance);
    #endregion
		
		public DataClassesDataContext() : 
				base(global::DatabaseAccess.Properties.Settings.Default.MyGraduationProject_DatabaseConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DataClassesDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClassesDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClassesDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClassesDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Item> Items
		{
			get
			{
				return this.GetTable<Item>();
			}
		}
		
		public System.Data.Linq.Table<UsersAdress> UsersAdresses
		{
			get
			{
				return this.GetTable<UsersAdress>();
			}
		}
		
		public System.Data.Linq.Table<User> Users
		{
			get
			{
				return this.GetTable<User>();
			}
		}
		
		public System.Data.Linq.Table<State> States
		{
			get
			{
				return this.GetTable<State>();
			}
		}
		
		public System.Data.Linq.Table<Role> Roles
		{
			get
			{
				return this.GetTable<Role>();
			}
		}
		
		public System.Data.Linq.Table<ReservationStatuse> ReservationStatuses
		{
			get
			{
				return this.GetTable<ReservationStatuse>();
			}
		}
		
		public System.Data.Linq.Table<Reservation> Reservations
		{
			get
			{
				return this.GetTable<Reservation>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Items")]
	public partial class Item : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ITEM_ID;
		
		private string _NAME;
		
		private string _DESCRPTION;
		
		private string _PHOTO;
		
		private System.Nullable<decimal> _PRICE_PER_DAY;
		
		private System.Nullable<int> _STATE_ID;
		
		private EntitySet<Reservation> _Reservations;
		
		private EntityRef<State> _State;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnITEM_IDChanging(int value);
    partial void OnITEM_IDChanged();
    partial void OnNAMEChanging(string value);
    partial void OnNAMEChanged();
    partial void OnDESCRPTIONChanging(string value);
    partial void OnDESCRPTIONChanged();
    partial void OnPHOTOChanging(string value);
    partial void OnPHOTOChanged();
    partial void OnPRICE_PER_DAYChanging(System.Nullable<decimal> value);
    partial void OnPRICE_PER_DAYChanged();
    partial void OnSTATE_IDChanging(System.Nullable<int> value);
    partial void OnSTATE_IDChanged();
    #endregion
		
		public Item()
		{
			this._Reservations = new EntitySet<Reservation>(new Action<Reservation>(this.attach_Reservations), new Action<Reservation>(this.detach_Reservations));
			this._State = default(EntityRef<State>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ITEM_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ITEM_ID
		{
			get
			{
				return this._ITEM_ID;
			}
			set
			{
				if ((this._ITEM_ID != value))
				{
					this.OnITEM_IDChanging(value);
					this.SendPropertyChanging();
					this._ITEM_ID = value;
					this.SendPropertyChanged("ITEM_ID");
					this.OnITEM_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NAME", DbType="NChar(20)")]
		public string NAME
		{
			get
			{
				return this._NAME;
			}
			set
			{
				if ((this._NAME != value))
				{
					this.OnNAMEChanging(value);
					this.SendPropertyChanging();
					this._NAME = value;
					this.SendPropertyChanged("NAME");
					this.OnNAMEChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DESCRPTION", DbType="NVarChar(MAX)")]
		public string DESCRPTION
		{
			get
			{
				return this._DESCRPTION;
			}
			set
			{
				if ((this._DESCRPTION != value))
				{
					this.OnDESCRPTIONChanging(value);
					this.SendPropertyChanging();
					this._DESCRPTION = value;
					this.SendPropertyChanged("DESCRPTION");
					this.OnDESCRPTIONChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PHOTO", DbType="NVarChar(MAX)")]
		public string PHOTO
		{
			get
			{
				return this._PHOTO;
			}
			set
			{
				if ((this._PHOTO != value))
				{
					this.OnPHOTOChanging(value);
					this.SendPropertyChanging();
					this._PHOTO = value;
					this.SendPropertyChanged("PHOTO");
					this.OnPHOTOChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PRICE_PER_DAY", DbType="Decimal(9,2)")]
		public System.Nullable<decimal> PRICE_PER_DAY
		{
			get
			{
				return this._PRICE_PER_DAY;
			}
			set
			{
				if ((this._PRICE_PER_DAY != value))
				{
					this.OnPRICE_PER_DAYChanging(value);
					this.SendPropertyChanging();
					this._PRICE_PER_DAY = value;
					this.SendPropertyChanged("PRICE_PER_DAY");
					this.OnPRICE_PER_DAYChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_STATE_ID", DbType="Int")]
		public System.Nullable<int> STATE_ID
		{
			get
			{
				return this._STATE_ID;
			}
			set
			{
				if ((this._STATE_ID != value))
				{
					if (this._State.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnSTATE_IDChanging(value);
					this.SendPropertyChanging();
					this._STATE_ID = value;
					this.SendPropertyChanged("STATE_ID");
					this.OnSTATE_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Item_Reservation", Storage="_Reservations", ThisKey="ITEM_ID", OtherKey="ITEM_ID")]
		public EntitySet<Reservation> Reservations
		{
			get
			{
				return this._Reservations;
			}
			set
			{
				this._Reservations.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="State_Item", Storage="_State", ThisKey="STATE_ID", OtherKey="STATE_ID", IsForeignKey=true)]
		public State State
		{
			get
			{
				return this._State.Entity;
			}
			set
			{
				State previousValue = this._State.Entity;
				if (((previousValue != value) 
							|| (this._State.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._State.Entity = null;
						previousValue.Items.Remove(this);
					}
					this._State.Entity = value;
					if ((value != null))
					{
						value.Items.Add(this);
						this._STATE_ID = value.STATE_ID;
					}
					else
					{
						this._STATE_ID = default(Nullable<int>);
					}
					this.SendPropertyChanged("State");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Reservations(Reservation entity)
		{
			this.SendPropertyChanging();
			entity.Item = this;
		}
		
		private void detach_Reservations(Reservation entity)
		{
			this.SendPropertyChanging();
			entity.Item = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.UsersAdresses")]
	public partial class UsersAdress : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ADRESS_ID;
		
		private string _STREET_NAME;
		
		private string _STREET_NUMBER;
		
		private System.Nullable<short> _POSSESION_NUMBER;
		
		private EntitySet<User> _Users;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnADRESS_IDChanging(int value);
    partial void OnADRESS_IDChanged();
    partial void OnSTREET_NAMEChanging(string value);
    partial void OnSTREET_NAMEChanged();
    partial void OnSTREET_NUMBERChanging(string value);
    partial void OnSTREET_NUMBERChanged();
    partial void OnPOSSESION_NUMBERChanging(System.Nullable<short> value);
    partial void OnPOSSESION_NUMBERChanged();
    #endregion
		
		public UsersAdress()
		{
			this._Users = new EntitySet<User>(new Action<User>(this.attach_Users), new Action<User>(this.detach_Users));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ADRESS_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ADRESS_ID
		{
			get
			{
				return this._ADRESS_ID;
			}
			set
			{
				if ((this._ADRESS_ID != value))
				{
					this.OnADRESS_IDChanging(value);
					this.SendPropertyChanging();
					this._ADRESS_ID = value;
					this.SendPropertyChanged("ADRESS_ID");
					this.OnADRESS_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_STREET_NAME", DbType="VarChar(50)")]
		public string STREET_NAME
		{
			get
			{
				return this._STREET_NAME;
			}
			set
			{
				if ((this._STREET_NAME != value))
				{
					this.OnSTREET_NAMEChanging(value);
					this.SendPropertyChanging();
					this._STREET_NAME = value;
					this.SendPropertyChanged("STREET_NAME");
					this.OnSTREET_NAMEChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_STREET_NUMBER", DbType="VarChar(50)")]
		public string STREET_NUMBER
		{
			get
			{
				return this._STREET_NUMBER;
			}
			set
			{
				if ((this._STREET_NUMBER != value))
				{
					this.OnSTREET_NUMBERChanging(value);
					this.SendPropertyChanging();
					this._STREET_NUMBER = value;
					this.SendPropertyChanged("STREET_NUMBER");
					this.OnSTREET_NUMBERChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_POSSESION_NUMBER", DbType="SmallInt")]
		public System.Nullable<short> POSSESION_NUMBER
		{
			get
			{
				return this._POSSESION_NUMBER;
			}
			set
			{
				if ((this._POSSESION_NUMBER != value))
				{
					this.OnPOSSESION_NUMBERChanging(value);
					this.SendPropertyChanging();
					this._POSSESION_NUMBER = value;
					this.SendPropertyChanged("POSSESION_NUMBER");
					this.OnPOSSESION_NUMBERChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="UsersAdress_User", Storage="_Users", ThisKey="ADRESS_ID", OtherKey="ADRESS_ID")]
		public EntitySet<User> Users
		{
			get
			{
				return this._Users;
			}
			set
			{
				this._Users.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Users(User entity)
		{
			this.SendPropertyChanging();
			entity.UsersAdress = this;
		}
		
		private void detach_Users(User entity)
		{
			this.SendPropertyChanging();
			entity.UsersAdress = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Users")]
	public partial class User : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _USER_ID;
		
		private string _LOGIN;
		
		private string _PASSWORD;
		
		private string _NAME;
		
		private string _SURNAME;
		
		private System.Nullable<short> _AGE;
		
		private System.Nullable<int> _ADRESS_ID;
		
		private System.Nullable<int> _ROLE_ID;
		
		private EntitySet<Reservation> _Reservations;
		
		private EntityRef<UsersAdress> _UsersAdress;
		
		private EntityRef<Role> _Role;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnUSER_IDChanging(int value);
    partial void OnUSER_IDChanged();
    partial void OnLOGINChanging(string value);
    partial void OnLOGINChanged();
    partial void OnPASSWORDChanging(string value);
    partial void OnPASSWORDChanged();
    partial void OnNAMEChanging(string value);
    partial void OnNAMEChanged();
    partial void OnSURNAMEChanging(string value);
    partial void OnSURNAMEChanged();
    partial void OnAGEChanging(System.Nullable<short> value);
    partial void OnAGEChanged();
    partial void OnADRESS_IDChanging(System.Nullable<int> value);
    partial void OnADRESS_IDChanged();
    partial void OnROLE_IDChanging(System.Nullable<int> value);
    partial void OnROLE_IDChanged();
    #endregion
		
		public User()
		{
			this._Reservations = new EntitySet<Reservation>(new Action<Reservation>(this.attach_Reservations), new Action<Reservation>(this.detach_Reservations));
			this._UsersAdress = default(EntityRef<UsersAdress>);
			this._Role = default(EntityRef<Role>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_USER_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int USER_ID
		{
			get
			{
				return this._USER_ID;
			}
			set
			{
				if ((this._USER_ID != value))
				{
					this.OnUSER_IDChanging(value);
					this.SendPropertyChanging();
					this._USER_ID = value;
					this.SendPropertyChanged("USER_ID");
					this.OnUSER_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LOGIN", DbType="NChar(12)")]
		public string LOGIN
		{
			get
			{
				return this._LOGIN;
			}
			set
			{
				if ((this._LOGIN != value))
				{
					this.OnLOGINChanging(value);
					this.SendPropertyChanging();
					this._LOGIN = value;
					this.SendPropertyChanged("LOGIN");
					this.OnLOGINChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PASSWORD", DbType="NChar(20)")]
		public string PASSWORD
		{
			get
			{
				return this._PASSWORD;
			}
			set
			{
				if ((this._PASSWORD != value))
				{
					this.OnPASSWORDChanging(value);
					this.SendPropertyChanging();
					this._PASSWORD = value;
					this.SendPropertyChanged("PASSWORD");
					this.OnPASSWORDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NAME", DbType="NChar(20)")]
		public string NAME
		{
			get
			{
				return this._NAME;
			}
			set
			{
				if ((this._NAME != value))
				{
					this.OnNAMEChanging(value);
					this.SendPropertyChanging();
					this._NAME = value;
					this.SendPropertyChanged("NAME");
					this.OnNAMEChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SURNAME", DbType="NChar(20)")]
		public string SURNAME
		{
			get
			{
				return this._SURNAME;
			}
			set
			{
				if ((this._SURNAME != value))
				{
					this.OnSURNAMEChanging(value);
					this.SendPropertyChanging();
					this._SURNAME = value;
					this.SendPropertyChanged("SURNAME");
					this.OnSURNAMEChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AGE", DbType="SmallInt")]
		public System.Nullable<short> AGE
		{
			get
			{
				return this._AGE;
			}
			set
			{
				if ((this._AGE != value))
				{
					this.OnAGEChanging(value);
					this.SendPropertyChanging();
					this._AGE = value;
					this.SendPropertyChanged("AGE");
					this.OnAGEChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ADRESS_ID", DbType="Int")]
		public System.Nullable<int> ADRESS_ID
		{
			get
			{
				return this._ADRESS_ID;
			}
			set
			{
				if ((this._ADRESS_ID != value))
				{
					if (this._UsersAdress.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnADRESS_IDChanging(value);
					this.SendPropertyChanging();
					this._ADRESS_ID = value;
					this.SendPropertyChanged("ADRESS_ID");
					this.OnADRESS_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ROLE_ID", DbType="Int")]
		public System.Nullable<int> ROLE_ID
		{
			get
			{
				return this._ROLE_ID;
			}
			set
			{
				if ((this._ROLE_ID != value))
				{
					if (this._Role.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnROLE_IDChanging(value);
					this.SendPropertyChanging();
					this._ROLE_ID = value;
					this.SendPropertyChanged("ROLE_ID");
					this.OnROLE_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_Reservation", Storage="_Reservations", ThisKey="USER_ID", OtherKey="USER_ID")]
		public EntitySet<Reservation> Reservations
		{
			get
			{
				return this._Reservations;
			}
			set
			{
				this._Reservations.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="UsersAdress_User", Storage="_UsersAdress", ThisKey="ADRESS_ID", OtherKey="ADRESS_ID", IsForeignKey=true)]
		public UsersAdress UsersAdress
		{
			get
			{
				return this._UsersAdress.Entity;
			}
			set
			{
				UsersAdress previousValue = this._UsersAdress.Entity;
				if (((previousValue != value) 
							|| (this._UsersAdress.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._UsersAdress.Entity = null;
						previousValue.Users.Remove(this);
					}
					this._UsersAdress.Entity = value;
					if ((value != null))
					{
						value.Users.Add(this);
						this._ADRESS_ID = value.ADRESS_ID;
					}
					else
					{
						this._ADRESS_ID = default(Nullable<int>);
					}
					this.SendPropertyChanged("UsersAdress");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Role_User", Storage="_Role", ThisKey="ROLE_ID", OtherKey="ROLE_ID", IsForeignKey=true)]
		public Role Role
		{
			get
			{
				return this._Role.Entity;
			}
			set
			{
				Role previousValue = this._Role.Entity;
				if (((previousValue != value) 
							|| (this._Role.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Role.Entity = null;
						previousValue.Users.Remove(this);
					}
					this._Role.Entity = value;
					if ((value != null))
					{
						value.Users.Add(this);
						this._ROLE_ID = value.ROLE_ID;
					}
					else
					{
						this._ROLE_ID = default(Nullable<int>);
					}
					this.SendPropertyChanged("Role");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Reservations(Reservation entity)
		{
			this.SendPropertyChanging();
			entity.User = this;
		}
		
		private void detach_Reservations(Reservation entity)
		{
			this.SendPropertyChanging();
			entity.User = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.States")]
	public partial class State : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _STATE_ID;
		
		private string _STATE_NAME;
		
		private EntitySet<Item> _Items;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnSTATE_IDChanging(int value);
    partial void OnSTATE_IDChanged();
    partial void OnSTATE_NAMEChanging(string value);
    partial void OnSTATE_NAMEChanged();
    #endregion
		
		public State()
		{
			this._Items = new EntitySet<Item>(new Action<Item>(this.attach_Items), new Action<Item>(this.detach_Items));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_STATE_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int STATE_ID
		{
			get
			{
				return this._STATE_ID;
			}
			set
			{
				if ((this._STATE_ID != value))
				{
					this.OnSTATE_IDChanging(value);
					this.SendPropertyChanging();
					this._STATE_ID = value;
					this.SendPropertyChanged("STATE_ID");
					this.OnSTATE_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_STATE_NAME", DbType="NChar(15)")]
		public string STATE_NAME
		{
			get
			{
				return this._STATE_NAME;
			}
			set
			{
				if ((this._STATE_NAME != value))
				{
					this.OnSTATE_NAMEChanging(value);
					this.SendPropertyChanging();
					this._STATE_NAME = value;
					this.SendPropertyChanged("STATE_NAME");
					this.OnSTATE_NAMEChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="State_Item", Storage="_Items", ThisKey="STATE_ID", OtherKey="STATE_ID")]
		public EntitySet<Item> Items
		{
			get
			{
				return this._Items;
			}
			set
			{
				this._Items.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Items(Item entity)
		{
			this.SendPropertyChanging();
			entity.State = this;
		}
		
		private void detach_Items(Item entity)
		{
			this.SendPropertyChanging();
			entity.State = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Roles")]
	public partial class Role : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ROLE_ID;
		
		private string _NAME;
		
		private EntitySet<User> _Users;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnROLE_IDChanging(int value);
    partial void OnROLE_IDChanged();
    partial void OnNAMEChanging(string value);
    partial void OnNAMEChanged();
    #endregion
		
		public Role()
		{
			this._Users = new EntitySet<User>(new Action<User>(this.attach_Users), new Action<User>(this.detach_Users));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ROLE_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ROLE_ID
		{
			get
			{
				return this._ROLE_ID;
			}
			set
			{
				if ((this._ROLE_ID != value))
				{
					this.OnROLE_IDChanging(value);
					this.SendPropertyChanging();
					this._ROLE_ID = value;
					this.SendPropertyChanged("ROLE_ID");
					this.OnROLE_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NAME", DbType="NChar(20)")]
		public string NAME
		{
			get
			{
				return this._NAME;
			}
			set
			{
				if ((this._NAME != value))
				{
					this.OnNAMEChanging(value);
					this.SendPropertyChanging();
					this._NAME = value;
					this.SendPropertyChanged("NAME");
					this.OnNAMEChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Role_User", Storage="_Users", ThisKey="ROLE_ID", OtherKey="ROLE_ID")]
		public EntitySet<User> Users
		{
			get
			{
				return this._Users;
			}
			set
			{
				this._Users.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Users(User entity)
		{
			this.SendPropertyChanging();
			entity.Role = this;
		}
		
		private void detach_Users(User entity)
		{
			this.SendPropertyChanging();
			entity.Role = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.ReservationStatuses")]
	public partial class ReservationStatuse : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _STATUS_ID;
		
		private string _NAME;
		
		private EntitySet<Reservation> _Reservations;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnSTATUS_IDChanging(int value);
    partial void OnSTATUS_IDChanged();
    partial void OnNAMEChanging(string value);
    partial void OnNAMEChanged();
    #endregion
		
		public ReservationStatuse()
		{
			this._Reservations = new EntitySet<Reservation>(new Action<Reservation>(this.attach_Reservations), new Action<Reservation>(this.detach_Reservations));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_STATUS_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int STATUS_ID
		{
			get
			{
				return this._STATUS_ID;
			}
			set
			{
				if ((this._STATUS_ID != value))
				{
					this.OnSTATUS_IDChanging(value);
					this.SendPropertyChanging();
					this._STATUS_ID = value;
					this.SendPropertyChanged("STATUS_ID");
					this.OnSTATUS_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NAME", DbType="NChar(20)")]
		public string NAME
		{
			get
			{
				return this._NAME;
			}
			set
			{
				if ((this._NAME != value))
				{
					this.OnNAMEChanging(value);
					this.SendPropertyChanging();
					this._NAME = value;
					this.SendPropertyChanged("NAME");
					this.OnNAMEChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="ReservationStatuse_Reservation", Storage="_Reservations", ThisKey="STATUS_ID", OtherKey="STATUS_ID")]
		public EntitySet<Reservation> Reservations
		{
			get
			{
				return this._Reservations;
			}
			set
			{
				this._Reservations.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Reservations(Reservation entity)
		{
			this.SendPropertyChanging();
			entity.ReservationStatuse = this;
		}
		
		private void detach_Reservations(Reservation entity)
		{
			this.SendPropertyChanging();
			entity.ReservationStatuse = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Reservations")]
	public partial class Reservation : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _RESERVATION_ID;
		
		private System.Nullable<System.DateTime> _DATE_FROM;
		
		private System.Nullable<System.DateTime> _DATE_TO;
		
		private System.Nullable<System.DateTime> _ORDER_DATE;
		
		private System.Nullable<decimal> _OVERALL_PRICE;
		
		private System.Nullable<int> _USER_ID;
		
		private System.Nullable<int> _ITEM_ID;
		
		private System.Nullable<int> _STATUS_ID;
		
		private EntityRef<Item> _Item;
		
		private EntityRef<ReservationStatuse> _ReservationStatuse;
		
		private EntityRef<User> _User;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnRESERVATION_IDChanging(int value);
    partial void OnRESERVATION_IDChanged();
    partial void OnDATE_FROMChanging(System.Nullable<System.DateTime> value);
    partial void OnDATE_FROMChanged();
    partial void OnDATE_TOChanging(System.Nullable<System.DateTime> value);
    partial void OnDATE_TOChanged();
    partial void OnORDER_DATEChanging(System.Nullable<System.DateTime> value);
    partial void OnORDER_DATEChanged();
    partial void OnOVERALL_PRICEChanging(System.Nullable<decimal> value);
    partial void OnOVERALL_PRICEChanged();
    partial void OnUSER_IDChanging(System.Nullable<int> value);
    partial void OnUSER_IDChanged();
    partial void OnITEM_IDChanging(System.Nullable<int> value);
    partial void OnITEM_IDChanged();
    partial void OnSTATUS_IDChanging(System.Nullable<int> value);
    partial void OnSTATUS_IDChanged();
    #endregion
		
		public Reservation()
		{
			this._Item = default(EntityRef<Item>);
			this._ReservationStatuse = default(EntityRef<ReservationStatuse>);
			this._User = default(EntityRef<User>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RESERVATION_ID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int RESERVATION_ID
		{
			get
			{
				return this._RESERVATION_ID;
			}
			set
			{
				if ((this._RESERVATION_ID != value))
				{
					this.OnRESERVATION_IDChanging(value);
					this.SendPropertyChanging();
					this._RESERVATION_ID = value;
					this.SendPropertyChanged("RESERVATION_ID");
					this.OnRESERVATION_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DATE_FROM", DbType="DateTime")]
		public System.Nullable<System.DateTime> DATE_FROM
		{
			get
			{
				return this._DATE_FROM;
			}
			set
			{
				if ((this._DATE_FROM != value))
				{
					this.OnDATE_FROMChanging(value);
					this.SendPropertyChanging();
					this._DATE_FROM = value;
					this.SendPropertyChanged("DATE_FROM");
					this.OnDATE_FROMChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DATE_TO", DbType="DateTime")]
		public System.Nullable<System.DateTime> DATE_TO
		{
			get
			{
				return this._DATE_TO;
			}
			set
			{
				if ((this._DATE_TO != value))
				{
					this.OnDATE_TOChanging(value);
					this.SendPropertyChanging();
					this._DATE_TO = value;
					this.SendPropertyChanged("DATE_TO");
					this.OnDATE_TOChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ORDER_DATE", DbType="DateTime")]
		public System.Nullable<System.DateTime> ORDER_DATE
		{
			get
			{
				return this._ORDER_DATE;
			}
			set
			{
				if ((this._ORDER_DATE != value))
				{
					this.OnORDER_DATEChanging(value);
					this.SendPropertyChanging();
					this._ORDER_DATE = value;
					this.SendPropertyChanged("ORDER_DATE");
					this.OnORDER_DATEChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OVERALL_PRICE", DbType="Decimal(12,2)")]
		public System.Nullable<decimal> OVERALL_PRICE
		{
			get
			{
				return this._OVERALL_PRICE;
			}
			set
			{
				if ((this._OVERALL_PRICE != value))
				{
					this.OnOVERALL_PRICEChanging(value);
					this.SendPropertyChanging();
					this._OVERALL_PRICE = value;
					this.SendPropertyChanged("OVERALL_PRICE");
					this.OnOVERALL_PRICEChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_USER_ID", DbType="Int")]
		public System.Nullable<int> USER_ID
		{
			get
			{
				return this._USER_ID;
			}
			set
			{
				if ((this._USER_ID != value))
				{
					if (this._User.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnUSER_IDChanging(value);
					this.SendPropertyChanging();
					this._USER_ID = value;
					this.SendPropertyChanged("USER_ID");
					this.OnUSER_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ITEM_ID", DbType="Int")]
		public System.Nullable<int> ITEM_ID
		{
			get
			{
				return this._ITEM_ID;
			}
			set
			{
				if ((this._ITEM_ID != value))
				{
					if (this._Item.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnITEM_IDChanging(value);
					this.SendPropertyChanging();
					this._ITEM_ID = value;
					this.SendPropertyChanged("ITEM_ID");
					this.OnITEM_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_STATUS_ID", DbType="Int")]
		public System.Nullable<int> STATUS_ID
		{
			get
			{
				return this._STATUS_ID;
			}
			set
			{
				if ((this._STATUS_ID != value))
				{
					if (this._ReservationStatuse.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnSTATUS_IDChanging(value);
					this.SendPropertyChanging();
					this._STATUS_ID = value;
					this.SendPropertyChanged("STATUS_ID");
					this.OnSTATUS_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Item_Reservation", Storage="_Item", ThisKey="ITEM_ID", OtherKey="ITEM_ID", IsForeignKey=true)]
		public Item Item
		{
			get
			{
				return this._Item.Entity;
			}
			set
			{
				Item previousValue = this._Item.Entity;
				if (((previousValue != value) 
							|| (this._Item.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Item.Entity = null;
						previousValue.Reservations.Remove(this);
					}
					this._Item.Entity = value;
					if ((value != null))
					{
						value.Reservations.Add(this);
						this._ITEM_ID = value.ITEM_ID;
					}
					else
					{
						this._ITEM_ID = default(Nullable<int>);
					}
					this.SendPropertyChanged("Item");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="ReservationStatuse_Reservation", Storage="_ReservationStatuse", ThisKey="STATUS_ID", OtherKey="STATUS_ID", IsForeignKey=true)]
		public ReservationStatuse ReservationStatuse
		{
			get
			{
				return this._ReservationStatuse.Entity;
			}
			set
			{
				ReservationStatuse previousValue = this._ReservationStatuse.Entity;
				if (((previousValue != value) 
							|| (this._ReservationStatuse.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._ReservationStatuse.Entity = null;
						previousValue.Reservations.Remove(this);
					}
					this._ReservationStatuse.Entity = value;
					if ((value != null))
					{
						value.Reservations.Add(this);
						this._STATUS_ID = value.STATUS_ID;
					}
					else
					{
						this._STATUS_ID = default(Nullable<int>);
					}
					this.SendPropertyChanged("ReservationStatuse");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_Reservation", Storage="_User", ThisKey="USER_ID", OtherKey="USER_ID", IsForeignKey=true)]
		public User User
		{
			get
			{
				return this._User.Entity;
			}
			set
			{
				User previousValue = this._User.Entity;
				if (((previousValue != value) 
							|| (this._User.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._User.Entity = null;
						previousValue.Reservations.Remove(this);
					}
					this._User.Entity = value;
					if ((value != null))
					{
						value.Reservations.Add(this);
						this._USER_ID = value.USER_ID;
					}
					else
					{
						this._USER_ID = default(Nullable<int>);
					}
					this.SendPropertyChanged("User");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591