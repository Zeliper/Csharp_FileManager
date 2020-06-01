using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;

namespace WindowsFormsApplication1
{
    class FileManagerObject
    {
        public FileManagerObject(string i_Path)
        {
            m_Object = new FileInfo(i_Path);
            if(isDirectory){
                foreach (string instFilePath in Directory.GetFiles(m_Object.FullName))
                {
                    FileManagerObject instChild = new FileManagerObject(this, instFilePath);
                    m_Childs.Add(CreateEntityRef(instChild));
                }
                foreach (string instFilePath in Directory.GetDirectories(m_Object.FullName))
                {
                    FileManagerObject instChild = new FileManagerObject(this, instFilePath);
                    m_Childs.Add(CreateEntityRef(instChild));
                }
            }
        }
        private FileManagerObject(FileManagerObject i_Parent, string i_Path)
        {
            m_Parent.Entity = i_Parent;
            m_Object = new FileInfo(i_Path);
            if (isDirectory)
            {
                foreach (string instFilePath in Directory.GetFiles(m_Object.FullName))
                {
                    FileManagerObject instChild = new FileManagerObject(this, instFilePath);
                    m_Childs.Add(CreateEntityRef(instChild));
                }
                foreach (string instFilePath in Directory.GetDirectories(m_Object.FullName))
                {
                    FileManagerObject instChild = new FileManagerObject(this, instFilePath);
                    m_Childs.Add(CreateEntityRef(instChild));
                }
            }
        }
        //생성자(자동으로 하위 디렉토리 지정)

        protected FileInfo m_Object;
        public FileInfo Object
        {
            get { return m_Object; }
            set { m_Object = value; }
        }
        //본인 FileInfo 되는 꿈꿨음 하지만 어림도없지

        protected EntityRef<FileManagerObject> m_Parent = new EntityRef<FileManagerObject>();
        public FileManagerObject Parent
        {
            get { return m_Parent.Entity; }
            set {
                EntityRef<FileManagerObject> inst = new EntityRef<FileManagerObject>();
                inst.Entity = value;
                m_Parent = inst; 
            }
        }
        //부모 객체

        protected List<EntityRef<FileManagerObject>> m_Childs = new List<EntityRef<FileManagerObject>>();
        public List<EntityRef<FileManagerObject>> Childs { get { return m_Childs; } }
        public void AddChild(FileManagerObject I_Child){
            m_Childs.Add(CreateEntityRef(I_Child));
        }
        //자식 객체 생성

        private EntityRef<FileManagerObject> CreateEntityRef(FileManagerObject i_Object)
        {
            EntityRef<FileManagerObject> inst = new EntityRef<FileManagerObject>();
            inst.Entity = i_Object;
            return inst;
        }
        //EntityRef 객체 생성 함수

        public string Name
        {
            get { return m_Object.Name; }
        }

        public bool isDirectory
        {
            get
            {
                FileAttributes instAtt = File.GetAttributes(m_Object.FullName);
                if (instAtt.HasFlag(FileAttributes.Directory))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        //디렉토리인지 확인하는 함수

        public bool isFile
        {
            get
            {
                return !isDirectory;
            }
        }
        //파일인지 확인하는 함수
    }
}
